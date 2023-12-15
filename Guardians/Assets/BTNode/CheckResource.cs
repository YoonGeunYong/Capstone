using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class CheckResource : Conditional
{
    public SharedGameObject baseGameObject;
    public int requiredResources = 10;

    public override TaskStatus OnUpdate()
    {
        if (baseGameObject == null || baseGameObject.Value == null)
        {
            Debug.LogError("baseGameObject is null");

            return TaskStatus.Failure;
        }

        Base baseScript = baseGameObject.Value.GetComponent<Base>();

        if (baseScript == null)
        {
            Debug.LogError("baseScript is null");

            return TaskStatus.Failure;
        }

        // �ڿ��� ������� Ȯ��
        if (baseScript.GetResource() >= requiredResources)
        {
            return TaskStatus.Success; // ����� ���
        }
        else
        {
            Debug.Log("Not enough resources");

            GameController.instance.EndAITurn();
            return TaskStatus.Failure; // ������ ���
        }
    }
}
