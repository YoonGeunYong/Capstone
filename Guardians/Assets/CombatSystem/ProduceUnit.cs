using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class ProduceUnit : Action
{
    public int unitCost = 10; // ���� ���� ���


    public override TaskStatus OnUpdate()
    {
        // ���� �ڿ��� Ȯ��
        int currentResources = GetResources();

        Debug.Log("���� �ڿ�: " + currentResources);
        // ����� �ڿ��� �ִ��� Ȯ��
        if (currentResources >= unitCost)
        {
            // ����� �ڿ��� �ִٸ� ���� ���� ������ ���⿡ �߰�
            ProduceNewUnit();

            return TaskStatus.Success; // ���� ���� ���� �� ���� ��ȯ
        }
        else
        {
            return TaskStatus.Failure; // �ڿ��� �����ϴٸ� ���� ��ȯ
        }
    }

    private void ProduceNewUnit()
    {
        // ���� ���� ������ ���⿡ �߰�
        GameController.instance.isEnemyTurn = false;
        
    }

    private int GetResources()
    {
        Debug.Log("���� �ڿ�: " + GameController.instance.enemyBase.resources);
        return GameController.instance.enemyBase.resources;
    }
}
