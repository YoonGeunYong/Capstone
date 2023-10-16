using UnityEngine;

public class Unit2 : MonoBehaviour
{
    public string unitName;
    public Vector2Int position;

    // Move the unit to a new position on the game board.
    public void MoveTo(Vector2Int newPosition)
    {
        Board2 board = GameController2.instance.gameBoard;

        // Check if the new position is valid and not occupied by another unit.
        Tile2 newTile = board.GetTileAt(newPosition);
        if (newTile != null && newTile.unit == null)
        {
            // Update the positions of the old and new tiles.
            Tile2 oldTile = board.GetTileAt(position);
            oldTile.unit = null;
            newTile.unit = this;

            // Update the position of the unit.
            position = newPosition;

            // Perform any additional actions related to moving the unit.

            Debug.Log(unitName + " moved to " + newPosition);
        }
    }

    // Perform an action or ability associated with this unit.
    public void PerformAction()
    {
        // Implement your custom logic for performing an action or ability here.

        Debug.Log(unitName + " performed an action.");
    }
}
