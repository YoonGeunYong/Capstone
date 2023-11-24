using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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


    public void SpawnUnit(GameObject UnitUI, Unit.Team team)
    {
        GameObject newUnitUI = Instantiate(UnitUI);

        Vector3 offset = new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f));
        Unit unit = newUnitUI.GetComponent<UnitUI>().unit;
        unit.team = team;
      

        GameObject newUnit = Instantiate(unit.gameObject, new Vector3(playerPosition.x, 0.5f, playerPosition.y)
            + offset, unit.transform.rotation);

        newUnitUI.GetComponent<UnitUI>().unit = newUnit.GetComponent<Unit>();

        MiniMap.instance.AddUnitToMinimap(newUnitUI.GetComponent<UnitUI>(), newUnit, MiniMap.instance.miniMapTiles[playerPosition.x, playerPosition.y]);

        GameController.instance.isPlayerTurn = false;
        Debug.Log("Player turn ended");
    }


    public Vector2Int GetPlayerPosition()
    {
        return playerPosition;
    }
}
