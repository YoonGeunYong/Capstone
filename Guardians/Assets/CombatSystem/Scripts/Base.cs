using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Base : MonoBehaviour
{
    public Vector2Int position;

    public int        resources;
    public int        resourcePerTurn;
    public int        maxResources;


    private void Start()
    {
        if (gameObject.name == "PlayerBase(Clone)")
            resources = 10;

        resourcePerTurn = 10;

        maxResources = 500;

    }


    public void InitPosition(Vector2Int basePosition)
    {

        position = basePosition;

    }


    public void SpawnUnit(GameObject unitUIPrefab, Unit.Team team, Vector2Int position, UnitTypes type)
    {

        GameObject newUnitUI = Instantiate(unitUIPrefab);

        if (newUnitUI == null)
        {
            Debug.LogError("Failed to instantiate UnitUI.");
            return;
        }

        UnitUI unitUIComponent = newUnitUI.GetComponent<UnitUI>();

        if (unitUIComponent == null)
        {
            Debug.LogError("UnitUI component not found on instantiated object.");
            return;
        }

        Unit unit = unitUIComponent.unit;

        unit.team = team;

        GameObject newUnit = Instantiate(unit.gameObject, 
            (team == Unit.Team.Player) ? new Vector3(position.x -100, position.y -100, 0f) :
                new Vector3(position.x -60, position.y -60, 0f) + RandomOffset(), unit.transform.rotation);
        
        if(newUnit.GetComponent<Rabbit>().team == Unit.Team.Enemy) // 12.12 enemy spawn scale fix
            newUnit.transform.localScale = new Vector3(0.3f, 0.3f, -0.3f);

        Unit newUnitComponent = newUnit.GetComponent<Unit>();

        if (newUnitComponent == null)
        {
            Debug.LogError("Unit component not found on instantiated object.");

            Destroy(newUnitUI);

            Destroy(newUnit);

            return;
        }

        unitUIComponent.unit = newUnitComponent;
        unitUIComponent.unit.statsSO = GameController.instance.preStats[(int)type];

        if(team == Unit.Team.Player)
        {
            MiniMap.instance.AddUnitToMinimap(unitUIComponent, newUnit, MiniMap.instance.miniMapTiles[0, 0]);
        }
            

        else if(team == Unit.Team.Enemy)
        {
            MiniMap.instance.AddUnitToMinimap(unitUIComponent, newUnit, MiniMap.instance.
                miniMapTiles[GameController.instance.width - 1, GameController.instance.height - 1]);

        }


        if (TrySpendResources(unit.stats.cost))
        {
            resources -= unit.stats.cost;
        }


        else
        {
            Debug.LogWarning("Failed to spawn unit. Insufficient resources.");

            Destroy(newUnitUI);

            Destroy(newUnit);
        }
    }


    public bool TrySpendResources(int amount)
    {

        if (resources >= amount)
        {
            resources -= amount;

            return true;
        }

        return false;

    }


    private Vector3 RandomOffset()
    {

        return new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f), 0f);

    }


    public Vector2Int GetPosition()
    {

        return position;

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