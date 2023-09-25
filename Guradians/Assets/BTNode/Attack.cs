using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Attack : Action
{

	EnemyAI enemyAI;
	public override void OnStart()
	{
		enemyAI = GetComponent<EnemyAI>();
	}

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}