using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MiniMapTile : MonoBehaviour
{
    public Dictionary<UnitTypes, List<Unit>> unitDictionary = new();
    public Dictionary<UnitTypes, List<UnitUI>> unitUIDictionary = new();
    
    public Vector2Int       gridPosition;
    public Vector2Int       boardPosition;
    public Vector2Int       originalPosition;
    
    public Vector2Int boardIndex;
    public List<Unit> includedUnits = new();
    
    public bool             IsMovable { get; set; }  
    public List<UnitUI>     unitsOnTile = new List<UnitUI>();

    private void Awake()
    {
        foreach (UnitTypes value in Enum.GetValues(typeof(UnitTypes)))
        {
            Debug.Log( value + " is added to unitDictionary");
            unitDictionary.Add(value, new List<Unit>());
            unitUIDictionary.Add(value, new List<UnitUI>());
        }
    }

    public void AddUnit(UnitUI unitUI)
    {

        unitsOnTile.    Add(unitUI);
        //unitUI.         CurrentTile = this;  

        //UpdateUnitPositions();

    }


    public void RemoveUnit(UnitUI unitUI)
    {

        unitsOnTile.    Remove(unitUI);
        //unitUI.         CurrentTile = null;  

        //UpdateUnitPositions();

    }

    public void UpdateUnitPositions()
    {
        
        for (int i = 0; i < unitsOnTile.Count; i++)
        {
            Vector3 offset                      = new Vector3(i * 1f, 0, 0);

            unitsOnTile[i].transform.position   = unitsOnTile[i].CurrentTile.transform.position + offset;

        }

    }

    public List<Unit> GetEnemies(Unit.Team myTeam)
    {
        List<Unit> enemies = new List<Unit>();

        foreach (UnitUI unitUI in unitsOnTile)
        {
            if (unitUI.unit.team != myTeam) // �ڽ��� ���� �ٸ� ���� ������ �ν�
            {
                Debug.Log("Enemy detected");
                enemies.Add(unitUI.unit);
            }
        }

        return enemies;
    }


    void OnMouseDown()
    {
        // �÷��̾� ������ Ȯ��
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
