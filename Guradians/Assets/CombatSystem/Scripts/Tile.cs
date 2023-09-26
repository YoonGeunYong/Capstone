using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int gridPosition;
    public Unit unitOnTile;
    public Board board;
/*
    public void OnMouseDown()
    {
        if (unitOnTile != null)
        {
            board.SelectUnit(unitOnTile);
        }
        else
        {
            board.MoveSelectedUnitTo(gridPosition);
        }
    }
*/
    public bool IsEmpty()
    {
        return unitOnTile == null;
    }

}
