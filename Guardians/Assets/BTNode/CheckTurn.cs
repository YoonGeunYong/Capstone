using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class CheckTurn : Conditional
{
    public override TaskStatus OnUpdate()
    {
        if (GameController.instance.isEnemyTurn)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}