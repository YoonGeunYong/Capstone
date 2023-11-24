using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MiniMapTile_copy : MonoBehaviour
{
    public Vector2Int       gridPosition;
    public Vector2Int       boardPosition;
    public Vector2Int       originalPosition;
    public bool             IsMovable { get; set; }  
    public List<UnitUI_copy>     unitsOnTile = new List<UnitUI_copy>();
    

    public void AddUnit(UnitUI_copy unitUI)
    {

        unitsOnTile.    Add(unitUI);
        unitUI.         CurrentTile = this;  

        UpdateUnitPositions();

    }


    public void RemoveUnit(UnitUI_copy unitUI)
    {

        unitsOnTile.    Remove(unitUI);
        unitUI.         CurrentTile = null;  

        UpdateUnitPositions();

    }

    private void UpdateUnitPositions()
    {
        
        for (int i = 0; i < unitsOnTile.Count; i++)
        {

            Vector3 offset                      = new Vector3(i * 1f, 0, 0);

            unitsOnTile[i].transform.position   = unitsOnTile[i].CurrentTile.transform.position + offset;

        }

    }


    void OnMouseDown()
    {

        if (MiniMap_copy.instance.isTileSelected)
        {
            
            if (IsMovable)
            {
                MiniMap_copy.instance.MoveUnitTo(this);
            }
        }

        else
        {
            
            if (unitsOnTile.Count > 0)
            {
                MiniMap_copy.instance.selectedMiniMapTile = this;
                MiniMap_copy.instance.HighlightMovableTiles();
            }
        }

    }
}
