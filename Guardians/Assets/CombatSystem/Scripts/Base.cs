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
    public void SpawnUnit(GameObject unitUIObject)
    {
        // Instantiate a new UnitUI object
        GameObject newUnitUIObject = Instantiate(unitUIObject);
        UnitUI newUnitUI = newUnitUIObject.GetComponent<UnitUI>();

        // Spawn a new unit at the base's current position with a small random offset
        Vector3 offset = new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));
        GameObject newUnit = Instantiate(newUnitUI.unit, new Vector3(playerPosition.x, 0, playerPosition.y) + offset, Quaternion.identity);

        // Add the unitUI to the minimap
        MiniMap.instance.AddUnitToMinimap(newUnitUI, newUnit, MiniMap.instance.miniMapTiles[playerPosition.x, playerPosition.y]);
    }


    public Vector2Int GetPlayerPosition()
    {
        return playerPosition;
    }
}
