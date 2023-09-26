using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public GameObject unitUI; // This represents the unit on the minimap

    public void Move(Vector2Int direction)
    {
        // Add code here to move the unit in the given direction on the actual game board,
        // and update the position of unitUI accordingly.
    }

    public void OnMouseDown()
    {
        GameController.instance.SelectUnit(this);
    }
}
