using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class EnemyAI : Agent
{


    public override void Initialize()
    {
        
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        int unitType = actions.DiscreteActions[0];
        Vector3 attackPosition = Vector3.zero;  // TODO: Replace with actual position from continuous actions

        //CreateUnit(unitType);
        //SetAttackPosition(unitType, attackPosition);
    }

    public void CreateUnit(int unitType)
    {
        Debug.Log("Creating unit of type " + unitType);
    }

    public void SetAttackPosition(int unitType, Vector3 attackPosition)
    {
        Debug.Log("Setting attack position for unit of type " + unitType + " to " + attackPosition);
    }
}
