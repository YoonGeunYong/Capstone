using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{
    public static MiniMap instance;

    public        MiniMapTile       selectedMiniMapTile;
    public        MiniMapTile[,]    miniMapTiles;
    public        GameObject        miniMapTilePrefab;
    public        Text              costText;

    public        bool              isTileSelected = false;
    public        int               width;
    public        int               height;
    public        float             noiseScale = 1.0f; // 펄린 노이즈
    public        float             resourceThreshold = 0.5f;
    public        int               cost = 50;
    public        bool[]            turnCheck = new bool[3];


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
        costText.text = "자원/" + cost;
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

                GameObject  tileObject              = Instantiate(miniMapTilePrefab, new Vector3(adjustedX, adjustedY, 0f), 
                                                      Quaternion.identity);

                MiniMapTile tileComponent           = tileObject.GetComponent<MiniMapTile>();

                tileComponent.originalPosition      = new Vector2Int(x, y); // add this
                
                tileComponent.boardIndex            = new Vector2Int(x, y);

                miniMapTiles[x, y]                  = tileComponent;
            }

    }


    private Vector3 RandomPos(float offsetRange)
    {

        return new Vector3(UnityEngine.Random.Range(-offsetRange, offsetRange), 
            UnityEngine.Random.Range(-offsetRange, offsetRange), 0f);

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

        return new Vector3(tiles[x, y].transform.position.x, tiles[x, y].transform.position.y, 0f);

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
        // 선택된 타일이 없거나 선택된 타일에 유닛이 없는 경우, 이동을 중단.
        if (selectedMiniMapTile == null || selectedMiniMapTile.unitsOnTile.Count == 0) return;

        Vector2Int newBoardPosition = CalculateNewBoardPosition(miniMapTile);

        // 이동을 시작하는 타일에 있는 모든 유닛을 이동하는 타일로 옮김.
        List<UnitUI> unitsToMove = new List<UnitUI>(selectedMiniMapTile.unitsOnTile);

        foreach (UnitUI unitUI in unitsToMove)
        {
            // // 이전 타일에서 유닛을 제거.
            // unitUI.CurrentTile.RemoveUnit(unitUI);
            //
            // // 새로운 타일에 유닛을 추가.
            // miniMapTile.AddUnit(unitUI);
            
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
        if (costText is null) return;
        if (cost < 3000)
        {
            cost += 50 + (30 * (GameController.instance.tile[0].isCostTile + 
                                GameController.instance.tile[1].isCostTile + 
                                GameController.instance.tile[2].isCostTile));
            if (cost > 3000)
                cost = 3000;
            costText.text = "자원/" + cost;
        }
        else if (cost >= 3000)
        {
            cost = 3000;
            costText.text = "자원/" + cost;
        }
        //GameController.instance.isPlayerTurn = false;
        Debug.Log("Player turn ended");
    }





    private Vector2Int CalculateNewBoardPosition(MiniMapTile miniMapTile)
    {

        int         adjustmentFactor    = 100;
        int         scaleDownFactor     = 10;

        Vector2Int  newRawPos           = new Vector2Int((int)miniMapTile.transform.position.x - adjustmentFactor,
                                              (int)miniMapTile.transform.position.y - adjustmentFactor);


        return newRawPos / scaleDownFactor;

    }


    private void MoveUnitUI(UnitUI unitUI, MiniMapTile nextMiniMapTile)
    {
        Vector3 offset              = RandomPos(2);
        unitUI.transform.position   = nextMiniMapTile.transform.position + offset;
        unitUI.boardPosition        = CalculateNewBoardPosition(nextMiniMapTile);

        MiniMapTile oldTile         = unitUI.CurrentTile;
        
        var unitDict = oldTile.unitDictionary;
        var unitUIDict = oldTile.unitUIDictionary;
        
        nextMiniMapTile.AddUnit(unitUI);
        
        nextMiniMapTile.unitUIDictionary[unitUI.unit.unitTypes].Add(unitUI);
        nextMiniMapTile.unitDictionary[unitUI.unit.unitTypes].Add(unitUI.unit);
        
        oldTile.unitUIDictionary[unitUI.unit.unitTypes].Remove(unitUI);
        oldTile.unitDictionary[unitUI.unit.unitTypes].Remove(unitUI.unit);
        
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