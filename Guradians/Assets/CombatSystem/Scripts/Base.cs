using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    // Position on the board.
    private Vector2Int      playerPosition;
    private Vector2Int      enemyPosition;


    public void InitPlayerPosition(Vector2Int basePosition)
    {
        playerPosition = basePosition;

        Debug.Log("Base initialized at " + playerPosition);
    }
    
    public void InitEnemyPosition(Vector2Int basePosition)
    {
        enemyPosition = basePosition;

        Debug.Log("Base initialized at " + enemyPosition);
    }

    // This method creates a new unit at the base's position.
    public void SpawnUnit(GameObject unitUI)
    {
        UnitUI uUI = unitUI.GetComponent<UnitUI>();

        Instantiate(uUI.unit, new Vector3(playerPosition.x, 0, playerPosition.y), Quaternion.identity);
    }

    public Vector2Int GetPlayerPosition()
    {
        return playerPosition;
    }
}
