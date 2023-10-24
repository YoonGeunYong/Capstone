using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public static MiniMap instance;

    private       Dictionary<GameObject, GameObject>    unitToUIMap;

    public        UnitUI                                selectedUnitUI;
    public        MiniMapTile[,]                        miniMapTiles;
    public        GameObject                            miniMapTilePrefab;  



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

    private void Start()
    {

        unitToUIMap = new Dictionary<GameObject, GameObject>();

    }


    public void InitMiniMap(int width, int height)
    {

        miniMapTiles = new MiniMapTile[width, height];


        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {

                int adjustedX                = (x * 10) + 100;
                int adjustedY                = (y * 10) + 100;

                GameObject tileObject        = Instantiate(miniMapTilePrefab, new Vector3(adjustedX, 0, adjustedY), Quaternion.identity);
                MiniMapTile tileComponent    = tileObject.GetComponent<MiniMapTile>();

                tileComponent.gridPosition   = new Vector2Int(adjustedX, adjustedY);

                miniMapTiles[x, y]           = tileComponent;

            }

    }


    public void AddUnitToMinimap(GameObject unitUI, MiniMapTile[,] tiles)
    {

        var minimapIcon                 = Instantiate(unitUI);

        minimapIcon.transform.position  = GetMinimapPos(tiles, 0, 0);

        unitToUIMap[unitUI]             = minimapIcon;

    }


    private static Vector3 GetMinimapPos(MiniMapTile[,] tiles, int x, int y)
    {

        return new Vector3(tiles[x, y].gridPosition.x, 0, tiles[x, y].gridPosition.y);

    }


    private Vector2Int ConvertBoardPosToMinimapPos(Vector2Int boardPos)
    {

        // Implement this function based on how your board and minimap are related.
        throw new NotImplementedException();

    }


    public void UpdateUnitOnMinimap(GameObject unitObject, Vector2Int newPositionOnBoard)
    {

        if (unitToUIMap.TryGetValue(unitObject, out GameObject minimapIcon))
        {
            var minimapPos                  = ConvertBoardPosToMinimapPos(newPositionOnBoard);
            minimapIcon.transform.position  = new Vector3(minimapPos.x, 0, minimapPos.y);
        }
        else
        {
            Debug.LogError("No UI found for this unit on the minimap.");
        }

    }


    public void HighlightMovableTiles(UnitUI unitUI)
    {
        int x   = unitUI.boardPosition.x;
        int y   = unitUI.boardPosition.y;

        HighlightTile(x - 1, y);
        HighlightTile(x + 1, y);
        HighlightTile(x, y - 1);
        HighlightTile(x, y + 1);

        selectedUnitUI = unitUI;
    }


    private void HighlightTile(int x, int y)
    {

        if (x >= 0 && x < miniMapTiles.GetLength(0) &&
            y >= 0 && y < miniMapTiles.GetLength(1))
        {

            miniMapTiles[x, y].GetComponent<Renderer>().
                material.color                              = Color.green;
            miniMapTiles[x, y].IsMovable                    = true;

        }

    }


    public void MoveUnitTo(MiniMapTile miniMapTile)
    {

        if (selectedUnitUI == null) return;

        Vector2Int newBoardPosition     = CalculateNewBoardPosition(miniMapTile);

        MoveUnitUI(miniMapTile);

        MoveActualUnit(newBoardPosition);

        InitMovable();

    }


    private Vector2Int CalculateNewBoardPosition(MiniMapTile miniMapTile)
    {
        int adjustmentFactor    = 100;
        int scaleDownFactor     = 10;

        Vector2Int newRawPos    = new Vector2Int(miniMapTile.gridPosition.x - adjustmentFactor,
                                              miniMapTile.gridPosition.y - adjustmentFactor);

        // Scale down the position to match with the board.
        return newRawPos / scaleDownFactor;

    }


    private void MoveUnitUI(MiniMapTile miniMapTile)
    {

        selectedUnitUI.transform.position   = miniMapTile.transform.position;
        selectedUnitUI.boardPosition        = CalculateNewBoardPosition(miniMapTile);

    }


    private void MoveActualUnit(Vector2Int newBoardPos)
    {

        selectedUnitUI.unit.gameObject.transform.position = new Vector3(newBoardPos.x * 10, 0, newBoardPos.y * 10);

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
  
}