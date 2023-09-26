using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Vector2Int gridPosition;
    public RectTransform minimapRect;  // Assign this in the inspector.
    public GameObject unitUIPrefab;     // Assign this in the inspector.
    public GameObject borderGlow;
    public Unit unit;


    private GameObject unitUIInstance;

    private void Start()
    {
        gridPosition = new Vector2Int(-20, -20);
        minimapRect = MinimapManager.instance.minimapRect;
        
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

    private void OnMouseDown()
    {
        if (borderGlow.activeSelf)
        {
            // If the border is already glowing, clicking on another tile will move the unit.
            Vector2Int clickPosition = GetClickPosition();

            if (GameController.instance.board.IsValidMove(clickPosition))
            {
                GameController.instance.board.MoveUnit(unit, clickPosition);
                Deselect();
            }
        }
        else
        {
            Select();
        }
    }

    private void Select()
    {
        borderGlow.SetActive(true);
        GameController.instance.board.SelectUnit(this);
    }

    private void Deselect()
    {
        borderGlow.SetActive(false);
        GameController.instance.board.DeselectCurrent();
    }

    private Vector2Int GetClickPosition()
    {
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int gridPosition = new Vector2Int(Mathf.RoundToInt(clickPosition.x), Mathf.RoundToInt(clickPosition.y));
        return gridPosition;
    }

    public void DestroyUnit()
    {
        Destroy(unitUIInstance);
        Destroy(this.gameObject);
    }





}
