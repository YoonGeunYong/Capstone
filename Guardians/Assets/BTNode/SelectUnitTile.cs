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
            // ���õ� Ÿ���� ���ٸ� ���и� ��ȯ
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

        // �� �κп��� ���õ� Ÿ���� ��ȿ���� Ȯ���ϰ� ��ȯ
        if (selectedTileXValue >= 0 && selectedTileXValue < MiniMap.instance.width &&
            selectedTileYValue >= 0 && selectedTileYValue < MiniMap.instance.height)
        {
            Debug.Log("Selected Tile : " + selectedTileXValue + ", " + selectedTileYValue);

            return MiniMap.instance.miniMapTiles[selectedTileXValue, selectedTileYValue];
        }
        else
        {
            // ���õ� Ÿ���� ��ȿ���� �ʴٸ� null ��ȯ
            return null;
        }
    }

    // ������ ��ġ�� Ÿ�ϵ��� ����Ʈ ��ȯ
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
