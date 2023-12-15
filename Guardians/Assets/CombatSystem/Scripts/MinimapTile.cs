using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
#if TreeEditor
using TreeEditor;
#endif
using UnityEngine;
using UnityEngine.UIElements;

public class MiniMapTile : MonoBehaviour
{
    public Vector2Int       gridPosition;
    public Vector2Int       originalPosition;
    public List<UnitUI>     unitsOnTile;
    public List<UnitUI>     enemyUnitsOnTile;
    public Color            originalColor;
    public bool IsMovable   { get; set; }

    private bool isCameraHighlighted = false;

    

    private void Start()
    {

        gridPosition        = new Vector2Int((int)transform.position.x, (int)transform.position.y);

        unitsOnTile         = new List<UnitUI>();

        enemyUnitsOnTile    = new List<UnitUI>();

        originalColor       = GetComponent<Renderer>().material.color;

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
        //12.08 'on tile with enemy' check
        if (enemyUnitsOnTile.Count != 0 && unitsOnTile.Count != 0)
        {
            GameController.instance.isFight = true;
            unitUI.unit.animator.SetBool("Attack", true);

            foreach (UnitUI ui in enemyUnitsOnTile)
            {
                ui.unit.enemyCheck = true;
            }
        } // 'not here enemy' shoud be make it

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
        if(myTeam == Unit.Team.Player)
        {
            return GetEnemiesFromList(enemyUnitsOnTile);
        }

        else if(myTeam == Unit.Team.Enemy)
        {
            return GetEnemiesFromList(unitsOnTile);
        }

        else
        {
            return null;
        }
    }

    public List<Unit> GetEnemiesFromList(List<UnitUI> unitUIs)
    {
        List<Unit> enemies = new List<Unit>();
        
        foreach (UnitUI unitUI in unitUIs)
        {
            if(unitUI.unit.team == Unit.Team.Enemy)
            {
                enemies.Add(unitUI.unit);
            }
        }

        return enemies;
    }

    public void HighlightTile()
    {
        if (!isCameraHighlighted)
        {
            GetComponent<Renderer>().material.color = Color.red;
            isCameraHighlighted = true;
        }
    }

    public void UnhighlightTile()
    {
        if (isCameraHighlighted)
        {
            GetComponent<Renderer>().material.color = originalColor;
            isCameraHighlighted = false;
        }
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