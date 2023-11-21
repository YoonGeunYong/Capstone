using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;


public class EnemyBehaviorTree : BehaviorTree
{
    public BehaviorTree behaviorTree;

    // GameController�� isEnemyTurn ������ ���� SharedBool ����
    public SharedBool isEnemyTurn;

    /*public override void OnStart()
    {
        // isEnemyTurn ������ Blackboard�� ���
        SetVariable("isEnemyTurn", isEnemyTurn);

        // �ൿƮ�� �ʱ�ȭ
        base.OnStart();
    }*/

    // Conditional Abort ��忡 ���� ȣ��Ǵ� �޼���
    public void SetIsEnemyTurn(bool value)
    {
        // isEnemyTurn ���� ������Ʈ
        isEnemyTurn.Value = value;
    }
}
