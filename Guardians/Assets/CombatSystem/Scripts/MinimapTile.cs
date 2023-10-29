using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapTile : MonoBehaviour
{
    public Vector2Int gridPosition;
    public Vector2Int boardPosition;
    public Vector2Int originalPosition;
    public bool IsMovable { get; set; }  // Whether this tile is currently movable.
    public List<UnitUI> unitsOnTile = new List<UnitUI>();


    public void AddUnit(UnitUI unitUI)
    {
        unitsOnTile.Add(unitUI);
        unitUI.CurrentTile = this;  // 유닛이 현재 속한 타일을 설정합니다.
        UpdateUnitPositions();
    }

    public void RemoveUnit(UnitUI unitUI)
    {
        unitsOnTile.Remove(unitUI);
        unitUI.CurrentTile = null;  // 유닛이 더 이상 속하지 않는 타일을 null로 설정합니다.
        UpdateUnitPositions();
    }

    private void UpdateUnitPositions()
    {
        // 겹치지 않도록 유닛 위치를 조정하는 코드를 여기에 작성...
        for (int i = 0; i < unitsOnTile.Count; i++)
        {
            Vector3 offset = new Vector3(i * 1f, 0, 0);
            unitsOnTile[i].transform.position = unitsOnTile[i].CurrentTile.transform.position + offset;
        }
    }

    void OnMouseDown()
    {
        if (MiniMap.instance.isTileSelected)
        {
            // 선택된 타일이 있고, 이 타일이 이동 가능한 경우, 이동을 시작하는 타일의 모든 유닛을 이동합니다.
            if (IsMovable)
            {
                MiniMap.instance.MoveUnitTo(this);
            }
        }
        else
        {
            // 선택된 타일이 없고, 이 타일에 유닛이 있는 경우, 이 타일에 있는 모든 유닛을 선택하고 이동 가능한 타일을 표시합니다.
            if (unitsOnTile.Count > 0)
            {
                MiniMap.instance.selectedMiniMapTile = this;
                MiniMap.instance.HighlightMovableTiles();
            }
        }
    }


}
