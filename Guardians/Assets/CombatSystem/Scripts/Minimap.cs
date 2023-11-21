using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class MiniMap : MonoBehaviour
{
    public static MiniMap instance;

    public        MiniMapTile       selectedMiniMapTile;
    public        MiniMapTile[,]    miniMapTiles;
    public        GameObject        miniMapTilePrefab;

    public        bool              isTileSelected = false;
    public        int               width;
    public        int               height;
    public        float             noiseScale = 1.0f; // �޸� ������
    public        float             resourceThreshold = 0.5f; 


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

    }


    public void InitMiniMap(int width, int height)
    {

        this.width      = width;
        this.height     = height;

        miniMapTiles    = new MiniMapTile[width, height];


        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                int         adjustedX               = (x * 10) + 100;
                int         adjustedY               = (y * 10) + 100;

                GameObject  tileObject              = Instantiate(miniMapTilePrefab, new Vector3(adjustedX, 0, adjustedY), 
                                                      Quaternion.identity);

                MiniMapTile tileComponent           = tileObject.GetComponent<MiniMapTile>();

                tileComponent.gridPosition          = new Vector2Int(adjustedX, adjustedY);
                tileComponent.originalPosition      = new Vector2Int(x, y); // add this

                miniMapTiles[x, y]                  = tileComponent;
            }

    }


    private Vector3 RandomPos(float offsetRange)
    {

        return new Vector3(UnityEngine.Random.Range(-offsetRange, offsetRange), 0, 
            UnityEngine.Random.Range(-offsetRange, offsetRange));

    }


    public void AddUnitToMinimap(UnitUI unitUI, GameObject actualUnitObject, MiniMapTile tile)
    {

        Vector3 offset                  = RandomPos(2);
        unitUI.transform.position       = GetMinimapPos(miniMapTiles, tile.originalPosition.x, tile.originalPosition.y) + offset;
        //unitUI.unit                     = actualUnitObject;
        unitUI.CurrentTile              = tile;

        tile.AddUnit(unitUI);

    }


    private static Vector3 GetMinimapPos(MiniMapTile[,] tiles, int x, int y)
    {

        return new Vector3(tiles[x, y].gridPosition.x, 0, tiles[x, y].gridPosition.y);

    }


    public void HighlightMovableTiles()
    {

        if (isTileSelected) return;

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


    public void MoveUnitTo(MiniMapTile miniMapTile)
    {
        // ���õ� Ÿ���� ���ų� ���õ� Ÿ�Ͽ� ������ ���� ���, �̵��� �ߴ�.
        if (selectedMiniMapTile == null || selectedMiniMapTile.unitsOnTile.Count == 0) return;

        Vector2Int newBoardPosition = CalculateNewBoardPosition(miniMapTile);

        // �̵��� �����ϴ� Ÿ�Ͽ� �ִ� ��� ������ �̵��ϴ� Ÿ�Ϸ� �ű�.
        List<UnitUI> unitsToMove = new List<UnitUI>(selectedMiniMapTile.unitsOnTile);

        foreach (UnitUI unitUI in unitsToMove)
        {
            // ���� Ÿ�Ͽ��� ������ ����.
            unitUI.CurrentTile.RemoveUnit(unitUI);

            // ���ο� Ÿ�Ͽ� ������ �߰�.
            miniMapTile.AddUnit(unitUI);

            MoveUnitUI(unitUI, miniMapTile);

            // Check if there are enemies in the target tile
            List<Unit> enemies = miniMapTile.GetEnemies(unitUI.unit.team);
            if (enemies.Count != 0)
            {
                Debug.Log("enemies detected");
                // If there are enemies, start attacking
                unitUI.unit.Attack(enemies);
            }
            else
            {
                Debug.Log("no enemies detected");
                // If there are no enemies, start moving
                unitUI.unit.MoveTo(newBoardPosition);
            }
            
        }

        InitMovable();

        isTileSelected = false;
        GameController.instance.EndPlayerTurn();
    }





    private Vector2Int CalculateNewBoardPosition(MiniMapTile miniMapTile)
    {

        int         adjustmentFactor    = 100;
        int         scaleDownFactor     = 10;

        Vector2Int  newRawPos           = new Vector2Int(miniMapTile.gridPosition.x - adjustmentFactor,
                                              miniMapTile.gridPosition.y - adjustmentFactor);


        return newRawPos / scaleDownFactor;

    }


    private void MoveUnitUI(UnitUI unitUI, MiniMapTile miniMapTile)
    {

        Vector3 offset              = RandomPos(2);
        unitUI.transform.position   = miniMapTile.transform.position + offset;
        unitUI.boardPosition        = CalculateNewBoardPosition(miniMapTile);

        Debug.Log("UnitUI moved to " + unitUI.boardPosition);

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

}