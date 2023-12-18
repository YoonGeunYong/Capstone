using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Collections.Generic;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class SpawnUnitNode : Action
{
    public static bool IsNodeRunning { get; private set; } = false;

    public SharedGameObject baseGameObject;
    public List<GameObject> unitUIPrefab;
    SharedInt selectedUnitIndexVariable;

    public override TaskStatus OnUpdate()
    {
        IsNodeRunning = true;

        if (baseGameObject == null || baseGameObject.Value == null)
        {
            Debug.LogError("baseGameObject is null");

            IsNodeRunning = false;

            return TaskStatus.Failure;
        }

        Base baseScript = baseGameObject.Value.GetComponent<Base>();

        if (baseScript == null)
        {
            Debug.LogError("baseScript is null");

            IsNodeRunning = false;

            return TaskStatus.Failure;
        }

        selectedUnitIndexVariable = Owner.GetVariable("selectedUnitIndex") as SharedInt;

        // Get the selected unit index from ml-agent
        int selectedUnitIndexValue = selectedUnitIndexVariable.Value;


        // Check if there are enough resources to spawn the selected unit
        if (baseScript.GetResource() >= unitUIPrefab[0].GetComponent<UnitUI>().unit.stats.cost)
        {
            // Spawn the selected unit based on the index
            baseScript.SpawnUnit(unitUIPrefab[0], Unit.Team.Enemy, baseScript.position, (UnitTypes)selectedUnitIndexValue);

            // Return success once the unit is spawned

            EnemyAgent.instance.AddReward(0.1f);
            
            IsNodeRunning = false;

            return TaskStatus.Success;
        }
        else
        {
            Debug.Log("Not enough resources");

            IsNodeRunning = false;
            // Return failure if there are not enough resources
            return TaskStatus.Failure;
        }
    }
}
