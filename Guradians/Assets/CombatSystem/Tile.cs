using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int gridPosition;
    public Unit unitOnTile;

    public void OnMouseDown()
    {
        GameController.instance.MoveSelectedUnitTo(gridPosition);
    }

    public bool IsEmpty()
    {
        return unitOnTile == null;
    }

}
