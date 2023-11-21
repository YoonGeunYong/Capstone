using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Base : MonoBehaviour
{
    public Vector2Int      playerPosition;
    public Vector2Int      enemyPosition;

    public int resources; // �÷��̾� �Ǵ� �ΰ������� �ڿ�
    public int resourcePerTurn = 10; // �� ���� �� ��� �ڿ���



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

        SpendResource(unit.stats.coast);

        GameController.instance.EndPlayerTurn();

    }


    public Vector2Int GetPlayerPosition()
    {
        return playerPosition;
    }

    public void EndTurnAndGetResource()
    {
        resources += resourcePerTurn;
    }

    public bool SpendResource(int amount)
    {
        if (resources >= amount)
        {
            resources -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetResource()
    {
        return resources;
    }
}
