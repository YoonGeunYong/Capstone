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

        // 자원이 충분한지 확인
        if (baseScript.GetResource() >= requiredResources)
        {
            return TaskStatus.Success; // 충분한 경우
        }
        else
        {
            Debug.Log("Not enough resources");

            GameController.instance.EndAITurn();
            return TaskStatus.Failure; // 부족한 경우
        }
    }
}
