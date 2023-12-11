using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class MoveToAnotherTile : Action
{
    public override void OnStart()
    {
        MiniMap.instance.selectedMiniMapTile = null;
    }

    public override TaskStatus OnUpdate()
    {
        MiniMap.instance.selectedMiniMapTile = GetRandomTile();

        MiniMap.instance.EnemyMoveUnitTo(MiniMap.instance.miniMapTiles[3,4]);

        GameController.instance.EndAITurn();

        Debug.Log("MoveToAnotherTile");
        return TaskStatus.Success;
    }

    private MiniMapTile GetRandomTile()
    {
        int x = Random.Range(0, MiniMap.instance.width);
        int y = Random.Range(0, MiniMap.instance.height);

        return MiniMap.instance.miniMapTiles[4, 4];
    }
}
