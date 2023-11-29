using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MiniMapTile : MonoBehaviour
{
    public Vector2Int       gridPosition;
    public Vector2Int       boardPosition;
    public Vector2Int       originalPosition;
    public List<UnitUI>     unitsOnTile;
    public List<UnitUI>     enemyUnitsOnTile;
    public bool IsMovable   { get; set; }


    private void Start()
    {

        gridPosition        = new Vector2Int((int)transform.position.x, (int)transform.position.y);

        unitsOnTile         = new List<UnitUI>();

        enemyUnitsOnTile    = new List<UnitUI>();

    }


    public void AddUnit(UnitUI unitUI)
    {

        if(unitUI.unit.team == Unit.Team.Player)
        {
            unitsOnTile.Add(unitUI);
            unitUI.CurrentTile = this;

            UpdateUnitPositions();
        }

        else if(unitUI.unit.team == Unit.Team.Enemy)
        {
            enemyUnitsOnTile.Add(unitUI);
            unitUI.CurrentTile = this;

            UpdateUnitPositions();
        }

    }


    public void RemoveUnit(UnitUI unitUI)
    {

        if(unitUI.unit.team == Unit.Team.Player)
        {
            unitsOnTile.Remove(unitUI);
            unitUI.CurrentTile = null;

            UpdateUnitPositions();
        }

        else if(unitUI.unit.team == Unit.Team.Enemy)
        {
            enemyUnitsOnTile.Remove(unitUI);
            unitUI.CurrentTile = null;

            UpdateUnitPositions();
        }

    }


    private void UpdateUnitPositions()
    {

        for (int i = 0; i < unitsOnTile.Count; i++)
        {

            Vector3 offset                    = new Vector3(i * 1f, 0, 0);

            unitsOnTile[i].transform.position = unitsOnTile[i].CurrentTile.transform.position + offset;

        }

        for (int i = 0; i < enemyUnitsOnTile.Count; i++)
        {

            Vector3 offset                         = new Vector3(i * 1f, 0, 0);

            enemyUnitsOnTile[i].transform.position = enemyUnitsOnTile[i].CurrentTile.transform.position + offset;

        }
    }


    public List<Unit> GetEnemies(Unit.Team myTeam)
    {

        List<Unit> enemies = new List<Unit>();

        foreach (UnitUI unitUI in unitsOnTile)
        {
            if (unitUI.unit.team != myTeam) 
            {
                Debug.Log("Enemy detected");
                enemies.Add(unitUI.unit);
            }
        }

        return enemies;

    }


    void OnMouseDown()
    {
        
        if (GameController.instance.isPlayerTurn)
        {
            if (MiniMap.instance.isTileSelected)
            {
                if (IsMovable)
                {
                    MiniMap.instance.MoveUnitTo(this);
                }
            }
            else
            {
                if (unitsOnTile.Count > 0)
                {
                    MiniMap.instance.selectedMiniMapTile = this;
                    MiniMap.instance.HighlightMovableTiles();
                }
            }
        }

    }

}