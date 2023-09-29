using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Vector2Int gridPosition;
    public RectTransform minimapRect;  // Assign this in the inspector.
    public UnitUi unitUIPrefab;     // Change GameObject to UnitUI

    private UnitUi unitUIInstance;

    private void Start()
    {
        gridPosition = new Vector2Int(-20, -20);
        minimapRect = MinimapManager.instance.minimapRect;

        // Instantiate the unit UI image.
        unitUIInstance = Instantiate(unitUIPrefab, minimapRect);
	    unitUIInstance.unit = this;  // Set the reference to this unit in the Unit UI instance
	    unitUIInstance.UpdateMinimapPosition();
    }

    public void Move(Vector2Int direction)
    {
	    // Code for moving on game board...
	
	    unitUIInstance.UpdateMinimapPosition();
    }

    public List<Vector2Int> GetPossibleMoves()
    {
        List<Vector2Int> possibleMoves = new List<Vector2Int>();

        // Check the four possible directions: up, down, left, right.
        Vector2Int[] directions = new Vector2Int[]
        {
        new Vector2Int(0, 1),  // Up
        new Vector2Int(0, -1), // Down
        new Vector2Int(-1, 0), // Left
        new Vector2Int(1, 0)   // Right
        };

        Board board = GameController.instance.board;

        foreach (Vector2Int direction in directions)
        {
            Vector2Int newPos = gridPosition + direction;

            if (board.IsValidMove(newPos))
                possibleMoves.Add(newPos);
        }

        return possibleMoves;
    }

}
