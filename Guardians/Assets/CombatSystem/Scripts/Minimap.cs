using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class MiniMap : MonoBehaviour
{
    public static MiniMap instance;



    public MiniMapTile selectedMiniMapTile;
    public MiniMapTile[,] miniMapTiles;
    public GameObject miniMapTilePrefab;
    public bool isTileSelected = false;



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
        miniMapTiles = new MiniMapTile[width, height];

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                int adjustedX = (x * 10) + 100;
                int adjustedY = (y * 10) + 100;

                GameObject tileObject = Instantiate(miniMapTilePrefab, new Vector3(adjustedX, 0, adjustedY), Quaternion.identity);
                MiniMapTile tileComponent = tileObject.GetComponent<MiniMapTile>();

                tileComponent.gridPosition = new Vector2Int(adjustedX, adjustedY);
                tileComponent.originalPosition = new Vector2Int(x, y); // add this

                miniMapTiles[x, y] = tileComponent;
            }
    }

    private Vector3 RandomPos()
    {
        return new Vector3(UnityEngine.Random.Range(-2.0f, 2.0f), 0, UnityEngine.Random.Range(-2.0f, 2.0f));
    }

    public void AddUnitToMinimap(UnitUI unitUI, GameObject actualUnitObject, MiniMapTile tile)
    {
        // Add a small random offset to the position
        Vector3 offset = RandomPos();
        unitUI.transform.position = GetMinimapPos(miniMapTiles, tile.originalPosition.x, tile.originalPosition.y) + offset;
        unitUI.unit = actualUnitObject;
        unitUI.CurrentTile = tile;

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

            miniMapTiles[x, y].GetComponent<Renderer>().
                material.color = Color.green;
            miniMapTiles[x, y].IsMovable = true;

        }

    }


    public void MoveUnitTo(MiniMapTile miniMapTile)
    {
        // 선택된 타일이 없거나 선택된 타일에 유닛이 없는 경우, 이동을 중단합니다.
        if (selectedMiniMapTile == null || selectedMiniMapTile.unitsOnTile.Count == 0) return;

        Vector2Int newBoardPosition = CalculateNewBoardPosition(miniMapTile);

        // 이동을 시작하는 타일에 있는 모든 유닛을 이동하는 타일로 옮깁니다.
        List<UnitUI> unitsToMove = new List<UnitUI>(selectedMiniMapTile.unitsOnTile);
        foreach (UnitUI unitUI in unitsToMove)
        {
            // 이전 타일에서 유닛을 제거합니다.
            unitUI.CurrentTile.RemoveUnit(unitUI);

            // 새로운 타일에 유닛을 추가합니다.
            miniMapTile.AddUnit(unitUI);

            MoveUnitUI(unitUI, miniMapTile);

            StartCoroutine(MoveActualUnit(unitUI, newBoardPosition));
        }

        InitMovable();

        isTileSelected = false;
    }



    private Vector2Int CalculateNewBoardPosition(MiniMapTile miniMapTile)
    {
        int adjustmentFactor = 100;
        int scaleDownFactor = 10;

        Vector2Int newRawPos = new Vector2Int(miniMapTile.gridPosition.x - adjustmentFactor,
                                              miniMapTile.gridPosition.y - adjustmentFactor);

        // Scale down the position to match with the board.
        return newRawPos / scaleDownFactor;

    }


    private void MoveUnitUI(UnitUI unitUI, MiniMapTile miniMapTile)
    {
        Vector3 offset = RandomPos();
        unitUI.transform.position = miniMapTile.transform.position + offset;
        unitUI.boardPosition = CalculateNewBoardPosition(miniMapTile);
        Debug.Log("UnitUI moved to " + unitUI.boardPosition);
    }


    IEnumerator MoveActualUnit(UnitUI unitUI, Vector2Int newBoardPos)
    {
        float speed = 10f;  // Adjust this value to change the speed
        Vector3 offset = RandomPos();
        Vector3 targetPosition = new Vector3(newBoardPos.x * 10, 0, newBoardPos.y * 10) + offset;
        while (Vector3.Distance(unitUI.unit.transform.position, targetPosition) > 0.1f)
        {
            unitUI.unit.transform.position = Vector3.MoveTowards(unitUI.unit.transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;  // Wait until next frame
        }
        Debug.Log("Unit moved to " + newBoardPos);
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