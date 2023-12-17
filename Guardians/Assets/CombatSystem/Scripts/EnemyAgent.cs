using Unity.MLAgents;
using BehaviorDesigner.Runtime;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents.Policies;
using UnityEngine.UIElements;

public class EnemyAgent : Agent
{
    public static EnemyAgent instance;
    public BehaviorTree behaviorTree;

    private int selectedUnitIndexValue;
    private int selectedMoveTileIndexValue;
    private float SomeFactor = 0.1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (behaviorTree == null)
        {
            Debug.LogError("BehaviorTree component not found on the GameObject.", this);
        }
    }


    public override void OnEpisodeBegin()
    {
        if (behaviorTree.GetVariable("selectedUnitIndex") == null)
        {
            Debug.LogError("selectedUnitIndex variable not found in the behavior tree.");
        }

        
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (!behaviorTree.isActiveAndEnabled || !GameController.instance.isEnemyTurn)
        {
            return;
        }

        // ���õ� ���� �ε��� ����
        int selectedUnitIndexValue = Mathf.Clamp(actions.DiscreteActions[0], 0, 3);
        Debug.Log("selectedUnitIndexValue : " + selectedUnitIndexValue);
        behaviorTree.SetVariableValue("selectedUnitIndex", selectedUnitIndexValue);

        // ���õ� �̵� Ÿ�� �ε��� ����
        int selectedMoveTileIndexValue = Mathf.Clamp(actions.DiscreteActions[1], 0, 3);
        Debug.Log("selectedMoveTileIndexValue : " + selectedMoveTileIndexValue);
        behaviorTree.SetVariableValue("selectedMoveTileIndex", selectedMoveTileIndexValue);

        

        /*if (unitTiles.Count > 0)
        {
            int discreteAction = actions.DiscreteActions[2];
            int selectedTileIndex = discreteAction % unitTiles.Count;

            int selectedX = unitTiles[selectedTileIndex].x;
            int selectedY = unitTiles[selectedTileIndex].y;

            Debug.Log("selectedTileX : " + selectedX);
            Debug.Log("selectedTileY : " + selectedY);

            Vector2Int selectedTile = new Vector2Int(selectedX, selectedY);

            if (unitTiles.Contains(selectedTile))
            {
                behaviorTree.SetVariableValue("selectedTileX", unitTiles[0].x);
                behaviorTree.SetVariableValue("selectedTileY", unitTiles[0].y);
                EndEpisode();
            }
            else
            {
                behaviorTree.SetVariableValue("selectedTileX", 0);
                behaviorTree.SetVariableValue("selectedTileY", 0);
            }
        }
        else
        {
            behaviorTree.SetVariableValue("selectedTileX", 0);
            behaviorTree.SetVariableValue("selectedTileY", 0);
        }*/

        Debug.Log("OnActionReceived");
        AddReward(-0.01f);
        EndEpisode();
    }

    




    public override void CollectObservations(VectorSensor sensor)
    {
        // �̴ϸ��� ���� Ÿ���� �ݺ�
        for (int x = 0; x < MiniMap.instance.width; x++)
        {
            for (int y = 0; y < MiniMap.instance.height; y++)
            {
                MiniMapTile tile = MiniMap.instance.miniMapTiles[x, y];

                // Ÿ�� ���� ���� �� �� ������ ���� ����
                if(tile.unitsOnTile.Count > 0)
                {
                    sensor.AddObservation(tile.unitsOnTile.Count * 0.01f);
                    sensor.AddObservation(tile.originalPosition);
                }

                if(tile.enemyUnitsOnTile.Count > 0)
                {
                    sensor.AddObservation(tile.enemyUnitsOnTile.Count * 0.01f);
                    sensor.AddObservation(tile.originalPosition);
                }
            }
        }
    }

    public float RewardFunc()
    {
        float reward = 0f;

        for (int i = 0; i < MiniMap.instance.width; i++)
        {
            for (int j = 0; j < MiniMap.instance.height; j++)
            {
                int enemyCount = MiniMap.instance.miniMapTiles[i, j].enemyUnitsOnTile.Count;

                // �� ������ �����ϴ� Ÿ�Ͽ� ���� ���� ���
                if (enemyCount > 0)
                {
                    reward += enemyCount * SomeFactor;  // SomeFactor�� ���� ������ ��� �Ǵ� �⺻ ����
                }
            }
        }

        return reward;
    }

}
