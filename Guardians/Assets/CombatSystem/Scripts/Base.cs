using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
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
        GameObject  newUnitUIObject     = Instantiate(unitUIObject);
        UnitUI      newUnitUI           = newUnitUIObject.GetComponent<UnitUI>();

        
        Vector3     offset              = new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));
        GameObject  newUnit             = Instantiate(newUnitUI.unit.gameObject, new Vector3(playerPosition.x, 0.5f, playerPosition.y) 
                                          + offset, newUnitUI.unit.transform.rotation);


        MiniMap.instance.AddUnitToMinimap(newUnitUI, newUnit, MiniMap.instance.miniMapTiles[playerPosition.x, playerPosition.y]);
    }


    public Vector2Int GetPlayerPosition()
    {
        return playerPosition;
    }
}
