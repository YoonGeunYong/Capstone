using Palmmedia.ReportGenerator.Core.Reporting.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
#if Unity_Editor
using UnityEditor.Experimental.GraphView;
#endif
using UnityEngine;


public class MiniMap : MonoBehaviour
{
    public static MiniMap instance;

    public MiniMapTile      selectedMiniMapTile;
    public MiniMapTile[,]   miniMapTiles;
    public GameObject       miniMapTilePrefab;

    public bool             isTileSelected;
    public int              width;
    public int              height;
    public float            noiseScale;
    public float            resourceThreshold;


    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Debug.LogError("More than one instance of MiniMap found!");
            Destroy(this.gameObject);
        }

        isTileSelected = false;

        noiseScale = 1.0f;

        resourceThreshold = 0.5f;

    }


    public void InitMiniMap(int width, int height)
    {

        this.width      =   width;
        this.height     =   height;

        miniMapTiles    =   new MiniMapTile[width, height];


        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                int adjustedX                   = (x * 10) + 100;
                int adjustedY                   = (y * 10) + 100;

                GameObject tileObject           = Instantiate(miniMapTilePrefab, new Vector3(adjustedX, adjustedY, 0f),
                                                      Quaternion.identity);

                MiniMapTile tileComponent       = tileObject.GetComponent<MiniMapTile>();

                tileComponent.gridPosition      = new Vector2Int(adjustedX, adjustedY);
                tileComponent.originalPosition  = new Vector2Int(x, y); // add this
                tileComponent.GetComponent<SpriteRenderer>().sprite = GameController.instance.board.miniMapTileSprite[(x * 5) + y];
                tileComponent.AddComponent<BoxCollider2D>();

                miniMapTiles[x, y]              = tileComponent;
            }

    }


    private Vector3 RandomPos(float offsetRange)
    {

        return new Vector3(UnityEngine.Random.Range(-offsetRange, offsetRange),
            UnityEngine.Random.Range(-offsetRange, offsetRange), 0);

    }


    public void AddUnitToMinimap(UnitUI unitUI, GameObject actualUnitObject, MiniMapTile tile)
    {

        Vector3 offset = RandomPos(2.0f);
        unitUI.transform.position = GetMinimapPos(miniMapTiles, tile.originalPosition.x, tile.originalPosition.y) + offset;
        //unitUI.unit                     = actualUnitObject;

        unitUI.CurrentTile = tile;

        tile.AddUnit(unitUI);

    }


    private static Vector3 GetMinimapPos(MiniMapTile[,] tiles, int x, int y)
    {

        return new Vector3(tiles[x, y].gridPosition.x, 0, tiles[x, y].gridPosition.y);

    }


    public void HighlightMovableTiles()
    {

        if (isTileSelected || GameController.instance.wasMoved) return;

        int x = selectedMiniMapTile.originalPosition.x;
        int y = selectedMiniMapTile.originalPosition.y;

        HighlightTile(x - 1, y);
        HighlightTile(x + 1, y);
        HighlightTile(x, y - 1);
        HighlightTile(x, y + 1);

        isTileSelected = true;
        
    }



    private void HighlightTile(int x, int y)
    {

        if (x >= 0 && x < miniMapTiles.GetLength(0) &&
            y >= 0 && y < miniMapTiles.GetLength(1))
        {

            miniMapTiles[x, y].GetComponent<Renderer>().material.color = Color.green;
            miniMapTiles[x, y].IsMovable = true;

        }

    }


    public void MoveUnitTo(MiniMapTile miniMapTile, bool isPlayer)
    {
        if ((isPlayer && selectedMiniMapTile?.unitsOnTile.Count == 0) ||
            (!isPlayer && selectedMiniMapTile?.enemyUnitsOnTile.Count == 0) ||
            GameController.instance.wasMoved)
        {
            return;
        }

        List<UnitUI> unitsToMove = isPlayer ? new List<UnitUI>(selectedMiniMapTile.unitsOnTile)
                                            : new List<UnitUI>(selectedMiniMapTile.enemyUnitsOnTile);

        Vector2Int newBoardPosition = CalculateNewBoardPosition(miniMapTile);

        foreach (UnitUI unitUI in unitsToMove)
        {
            unitUI.CurrentTile.RemoveUnit(unitUI);
            miniMapTile.AddUnit(unitUI);
            MoveUnitUI(unitUI, miniMapTile);

            unitUI.unit.MoveTo(newBoardPosition, miniMapTile);
        }

        if (isPlayer)
        {
            if(miniMapTile == MiniMap.instance.miniMapTiles[MiniMap.instance.width - 1, MiniMap.instance.height - 1])
            {
                GameController.instance.GameOver(Unit.Team.Enemy);
            }


            GameController.instance.wasMoved = true;
            InitMovable();
        }
        else if(!isPlayer)
        {
            if (miniMapTile == MiniMap.instance.miniMapTiles[0, 0])
            {
                GameController.instance.GameOver(Unit.Team.Player);
            }
        }

        isTileSelected = false;
        
    }


    private Vector2Int CalculateNewBoardPosition(MiniMapTile miniMapTile)
    {
        int indexX              = miniMapTile.originalPosition.x;
        int indexY              = miniMapTile.originalPosition.y;

        Vector2Int newBoardPos = Board.boardInstance.tiles[indexX, indexY].gridPosition;

        return newBoardPos;
    }


    private void MoveUnitUI(UnitUI unitUI, MiniMapTile miniMapTile)
    {

        Vector3 offset              = RandomPos(3.0f);
        unitUI.transform.position   = miniMapTile.transform.position + offset;
        unitUI.boardPosition        = CalculateNewBoardPosition(miniMapTile);

    }


    private void InitMovable()
    {
        foreach (var tile in miniMapTiles)
        {
            if (tile.IsMovable)
            {
                tile.GetComponent<Renderer>().material.color = Color.white;
                tile.IsMovable = false;
            }
        }
    }

    public MiniMapTile GetTileAt(Vector2Int position)
    {
        // Check if the position is within the bounds of the map
        if (position.x >= 0 && position.x < miniMapTiles.GetLength(0) &&
            position.y >= 0 && position.y < miniMapTiles.GetLength(1))
        {
            // Return the tile at the given position
            return miniMapTiles[position.x, position.y];
        }

        // If the position is out of bounds, return null
        return null;
    }

    public MiniMapTile GetMiniMapTile()
    {
        foreach (var tile in miniMapTiles)
        {
            if (tile.unitsOnTile.Count != 0 && tile.enemyUnitsOnTile.Count != 0)
            {
                return tile;
            }
        }
        return null;
    }

}