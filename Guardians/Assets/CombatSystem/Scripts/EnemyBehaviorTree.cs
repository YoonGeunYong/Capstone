using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;


public class EnemyBehaviorTree : BehaviorTree
{
    public BehaviorTree behaviorTree;

    // GameController의 isEnemyTurn 변수에 대한 SharedBool 변수
    public SharedBool isEnemyTurn;

    /*public override void OnStart()
    {
        // isEnemyTurn 변수를 Blackboard에 등록
        SetVariable("isEnemyTurn", isEnemyTurn);

        // 행동트리 초기화
        base.OnStart();
    }*/

    // Conditional Abort 노드에 의해 호출되는 메서드
    public void SetIsEnemyTurn(bool value)
    {
        // isEnemyTurn 변수 업데이트
        isEnemyTurn.Value = value;
    }
}
