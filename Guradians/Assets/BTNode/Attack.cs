using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Attack : Action
{

	//EnemyAI enemyAI;


	public override void OnStart()
	{
		//enemyAI = GetComponent<EnemyAI>();
		//enemyAI.RequestDecision();
	}

	public override TaskStatus OnUpdate()
	{
		//if(enemyAI.IsActionCompleted)
		//	return TaskStatus.Success;

		return TaskStatus.Running;
		
	}
}