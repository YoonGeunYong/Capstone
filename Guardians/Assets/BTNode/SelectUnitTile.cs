using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class SelectUnitTile : Action
{
    public static bool IsNodeRunning { get; private set; } = false;

    public SharedInt selectedTileX;
    public SharedInt selectedTileY;

    public override TaskStatus OnUpdate()
    {
        IsNodeRunning = true;

        MiniMap.instance.selectedMiniMapTile = GetTilesWithEnemyUnits();

        if (MiniMap.instance.selectedMiniMapTile != null)
        {
            IsNodeRunning = false;
            return TaskStatus.Success;
        }
        else
        {
            // 선택된 타일이 없다면 실패를 반환
            IsNodeRunning = false;
            return TaskStatus.Failure;
        }
    }

    private MiniMapTile GetSelectedTile()
    {
        selectedTileX = Owner.GetVariable("selectedTileX") as SharedInt;
        selectedTileY = Owner.GetVariable("selectedTileY") as SharedInt;

        int selectedTileXValue = selectedTileX.Value;
        int selectedTileYValue = selectedTileY.Value;

        // 이 부분에서 선택된 타일이 유효한지 확인하고 반환
        if (selectedTileXValue >= 0 && selectedTileXValue < MiniMap.instance.width &&
            selectedTileYValue >= 0 && selectedTileYValue < MiniMap.instance.height)
        {
            Debug.Log("Selected Tile : " + selectedTileXValue + ", " + selectedTileYValue);

            return MiniMap.instance.miniMapTiles[selectedTileXValue, selectedTileYValue];
        }
        else
        {
            // 선택된 타일이 유효하지 않다면 null 반환
            return null;
        }
    }

    // 유닛이 배치된 타일들의 리스트 반환
    private MiniMapTile GetTilesWithEnemyUnits()
    {
        List<MiniMapTile> unitTiles = new List<MiniMapTile>();

        for (int i = 0; i < MiniMap.instance.width; i++)
        {
            for (int j = 0; j < MiniMap.instance.height; j++)
            {
                if (MiniMap.instance.miniMapTiles[i, j].enemyUnitsOnTile.Count > 0)
                {
                    unitTiles.Add(MiniMap.instance.miniMapTiles[i, j]);
                }
            }
        }
        int randint = Random.Range(0, unitTiles.Count);

        return unitTiles[randint];
    }
}
