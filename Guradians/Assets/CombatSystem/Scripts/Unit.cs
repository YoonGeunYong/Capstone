using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private int minimapWidth;
    private int minimapHeight;

    public Vector2Int gridPosition;
    public RectTransform minimapRect;  // Assign this in the inspector.
    public GameObject unitUIPrefab;     // Assign this in the inspector.

    private GameObject unitUIInstance;

    private void Start()
    {
        gridPosition = new Vector2Int(-20, -20);
        minimapRect = MinimapManager.instance.minimapRect;

        // Assuming minimapRect is already assigned in the inspector
        minimapWidth = (int)minimapRect.rect.width;
        minimapHeight = (int)minimapRect.rect.height;

        // Instantiate the unit UI image.
        unitUIInstance = Instantiate(unitUIPrefab, minimapRect);
        UpdateMinimapPosition();
    }

    public void Move(Vector2Int direction)
    {
        // Add code here to move the unit in the given direction on the actual game board,
        // and update the position of unitUI accordingly.

        // Assume we have a reference to the game board and its tiles.
        Board board = GameController.instance.board;

        // Calculate new position.
        Vector2Int newPosition = this.gridPosition + direction;

        // Check if the move is valid (e.g., within bounds and not occupied).
        if (board.IsValidMove(newPosition))
        {
            // Update our position in the game board's data structure.
            board.MoveUnit(this, newPosition);

            // Update our position for rendering purposes.
            this.transform.position = new Vector3(newPosition.x, newPosition.y, 0);

            // Update unitUI's position as well. Convert newPosition to minimap coordinates somehow
            UpdateMinimapPosition();
        }
    }

    private void UpdateMinimapPosition()
    {
        Vector3 minimapPosition = ConvertToMinimapCoordinates(gridPosition);
        unitUIInstance.transform.localPosition = new Vector3(minimapPosition.x, minimapPosition.y, 0);
        Debug.Log("Minimap position: " + minimapPosition);

    }

    private Vector3 ConvertToMinimapCoordinates(Vector2Int boardPosition)
    {
        float adjustedX = boardPosition.x *= 4;
        float adjustedY = boardPosition.y *= 4;

        // The y-coordinate remains same because we are not changing it in gameboard
        return new Vector3(adjustedX, adjustedY, 0);
    }





}
