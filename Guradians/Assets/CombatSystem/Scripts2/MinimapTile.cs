using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapTile : MonoBehaviour
{
    public Vector2Int boardPosition;

    public void Init(Vector2Int boardPosition)
    {
        this.boardPosition = boardPosition;
    }

    private void OnMouseDown()
    {
        // When this minimap tile is clicked, move the selected unit to the corresponding position on the game board.
        Board board = GameController.instance.board;
        Unit selectedUnit = board.selectedUnit;

        if (selectedUnit != null)
            board.MoveSelectedUnitTo(boardPosition);
    }
}

