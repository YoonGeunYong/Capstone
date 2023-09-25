using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class EnemyAI : Agent
{
    public bool IsActionCompleted { get; private set; } = false;

    public override void Initialize()
    {
        
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int unitType = actions.DiscreteActions[0];
        Vector3 attackPosition = Vector3.zero;  // TODO: Replace with actual position from continuous actions

        if (unitType == 0)
            CreateUnit(unitType);
        else
            SetAttackPosition(unitType, attackPosition);

        IsActionCompleted = true;
    }

    public void CreateUnit(int unitType)
    {
        Debug.Log("Creating unit of type " + unitType);
        // TODO: Add the actual logic to create a unit.
        // After that, you might want to set IsActionCompleted to true.
        IsActionCompleted = true;

        // Alternatively, you could set IsActionCompleted to true in a callback function 
        // that is called when the creation process is completed.

        // If you do this, don't forget to reset IsActionCompleted back to false at appropriate times,
        // such as at the beginning of OnEpisodeBegin() or whenever RequestDecision() is called.
    }

    public void SetAttackPosition(int unitType, Vector3 attackPosition)
    {
        Debug.Log("Setting attack position for unit of type " + unitType + " to " + attackPosition);
        // TODO: Add the actual logic to set an attack position for a certain type of units.
        // After that, you might want to set IsActionCompleted to true.
        IsActionCompleted = true;

        // As in CreateUnit(), remember about resetting the flag and consider using callbacks if necessary.
    }
}
