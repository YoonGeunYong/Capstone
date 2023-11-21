using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class ProduceUnit : Action
{
    public int unitCost = 10; // 유닛 생산 비용


    public override TaskStatus OnUpdate()
    {
        // 현재 자원을 확인
        int currentResources = GetResources();

        Debug.Log("현재 자원: " + currentResources);
        // 충분한 자원이 있는지 확인
        if (currentResources >= unitCost)
        {
            // 충분한 자원이 있다면 유닛 생산 로직을 여기에 추가
            ProduceNewUnit();

            return TaskStatus.Success; // 유닛 생산 성공 시 성공 반환
        }
        else
        {
            return TaskStatus.Failure; // 자원이 부족하다면 실패 반환
        }
    }

    private void ProduceNewUnit()
    {
        // 유닛 생산 로직을 여기에 추가
        GameController.instance.isEnemyTurn = false;
        
    }

    private int GetResources()
    {
        Debug.Log("현재 자원: " + GameController.instance.enemyBase.resources);
        return GameController.instance.enemyBase.resources;
    }
}
