using System.Collections;
using System.Collections.Generic;
using TMPro;
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


    private static readonly Vector2Int spawnPositionIndex = new(0, 0);

    public void SpawnUnit(GameObject UnitUI, Unit.Team team, UnitTypes unitType = UnitTypes.Rabbit)
    {
        var instanceMiniMapTile = MiniMap.instance.miniMapTiles[0, 0];
        Vector3 offset = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), 0f);

        if (instanceMiniMapTile.unitUIDictionary[unitType].Count > 0)
        { // 이미 해당하는 유닛이 있다면
            GameObject newUnit = Instantiate(UnitUI.GetComponent<UnitUI>().unit.gameObject,
                new Vector3(playerPosition.x, playerPosition.y, 0f)
                + offset, UnitUI.GetComponent<UnitUI>().unit.transform.rotation);

            instanceMiniMapTile.unitDictionary[unitType].Add(newUnit.GetComponent<Unit>());
            
            instanceMiniMapTile.unitUIDictionary[unitType][0].GetComponentInChildren<TMP_Text>().text =
                instanceMiniMapTile.unitDictionary[unitType].Count.ToString();
        }
        else
        { //해당 되는 유닛을 새로 만든다면
            GameObject newUnitUI = Instantiate(UnitUI);
            
            Unit unit = newUnitUI.GetComponent<UnitUI>().unit;
            unit.team = team;
            
            instanceMiniMapTile.unitUIDictionary[unitType].Add(newUnitUI.GetComponent<UnitUI>());
            
            GameObject newUnit = Instantiate(unit.gameObject, new Vector3(playerPosition.x, playerPosition.y, 0f)
                                                              + offset, unit.transform.rotation);

            newUnitUI.GetComponent<UnitUI>().unit = newUnit.GetComponent<Unit>();
            newUnitUI.GetComponent<UnitUI>().CurrentTile = instanceMiniMapTile;
            
            instanceMiniMapTile.unitDictionary[unitType].Add(newUnit.GetComponent<Unit>());

            MiniMap.instance.AddUnitToMinimap(newUnitUI.GetComponent<UnitUI>(), newUnit,
                MiniMap.instance.miniMapTiles[playerPosition.x, playerPosition.y]);
        }


        //GameController.instance.isPlayerTurn = false;
        Debug.Log("Player turn ended");
    }


    public Vector2Int GetPlayerPosition()
    {
        return playerPosition;
    }
}
