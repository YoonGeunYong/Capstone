using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap_copy : MonoBehaviour
{
    public static MiniMap_copy instance;

    public        MiniMapTile_copy       selectedMiniMapTile;
    public        MiniMapTile_copy[,]    miniMapTiles;
    public        GameObject        miniMapTilePrefab;
    public        Text              costText;

    public        bool              isTileSelected = false;
    public        int               width;
    public        int               height;
    public        float             noiseScale = 1.0f; // �޸� ������
    public        float             resourceThreshold = 0.5f;
    public        int               cost = 50;
    public        int               checkCostTile;
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


    public void InitMiniMap(int width, int height)
    {

        this.width      = width;
        this.height     = height;

        miniMapTiles    = new MiniMapTile_copy[width, height];


        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                int         adjustedX               = (x * 10) + 100;
                int         adjustedY               = (y * 10) + 100;

                GameObject  tileObject              = Instantiate(miniMapTilePrefab, new Vector3(adjustedX, 0, adjustedY), 
                                                      Quaternion.identity);

                MiniMapTile_copy tileComponent           = tileObject.GetComponent<MiniMapTile_copy>();

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


    public void AddUnitToMinimap(UnitUI_copy unitUI, GameObject actualUnitObject, MiniMapTile_copy tile)
    {

        Vector3 offset                  = RandomPos(2);
        unitUI.transform.position       = GetMinimapPos(miniMapTiles, tile.originalPosition.x, tile.originalPosition.y) + offset;
        unitUI.unit                     = actualUnitObject;
        unitUI.CurrentTile              = tile;

        tile.AddUnit(unitUI);

    }


    private static Vector3 GetMinimapPos(MiniMapTile_copy[,] tiles, int x, int y)
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


    public void MoveUnitTo(MiniMapTile_copy miniMapTile)
    {

        // ���õ� Ÿ���� ���ų� ���õ� Ÿ�Ͽ� ������ ���� ���, �̵��� �ߴ�.
        if (selectedMiniMapTile == null || selectedMiniMapTile.unitsOnTile.Count == 0) return;

        Vector2Int newBoardPosition = CalculateNewBoardPosition(miniMapTile);

        // �̵��� �����ϴ� Ÿ�Ͽ� �ִ� ��� ������ �̵��ϴ� Ÿ�Ϸ� �ű�.
        List<UnitUI_copy> unitsToMove = new List<UnitUI_copy>(selectedMiniMapTile.unitsOnTile);

        foreach (UnitUI_copy unitUI in unitsToMove)
        {
            // ���� Ÿ�Ͽ��� ������ ����.
            unitUI.         CurrentTile.RemoveUnit(unitUI);

            // ���ο� Ÿ�Ͽ� ������ �߰�.
            miniMapTile.    AddUnit(unitUI);

            MoveUnitUI(unitUI, miniMapTile);

            StartCoroutine(MoveActualUnit(unitUI, newBoardPosition));
        }

        InitMovable();

        isTileSelected = false;
        if (costText is null) return;
        if (cost < 500)
        {
            cost += 50 + (30 * checkCostTile);
            costText.text = "�ڿ�/" + cost;
        }
        else if (cost > 500) { cost = 500; }
    }



    private Vector2Int CalculateNewBoardPosition(MiniMapTile_copy miniMapTile)
    {

        int         adjustmentFactor    = 100;
        int         scaleDownFactor     = 10;

        Vector2Int  newRawPos           = new Vector2Int(miniMapTile.gridPosition.x - adjustmentFactor,
                                              miniMapTile.gridPosition.y - adjustmentFactor);


        return newRawPos / scaleDownFactor;

    }


    private void MoveUnitUI(UnitUI_copy unitUI, MiniMapTile_copy miniMapTile)
    {

        Vector3 offset              = RandomPos(2);
        unitUI.transform.position   = miniMapTile.transform.position + offset;
        unitUI.boardPosition        = CalculateNewBoardPosition(miniMapTile);

        Debug.Log("UnitUI moved to " + unitUI.boardPosition);

    }


    IEnumerator MoveActualUnit(UnitUI_copy unitUI, Vector2Int newBoardPos)
    {
        yield return unitUI.unit.GetComponent<Unit_copy>().MoveTo(newBoardPos);
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