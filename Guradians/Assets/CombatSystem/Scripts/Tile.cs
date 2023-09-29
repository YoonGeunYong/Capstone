using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int gridPosition;
    public Unit unitOnTile;
    public Board board;
    public bool isAccessible = true;  // Whether a unit can move to this tile.

    private SpriteRenderer spriteRenderer;  // Used for changing the tile's color.

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool IsEmpty()
    {
        return unitOnTile == null;
    }

    // Call this method to change the color of the tile.
    public void Highlight(bool highlight)
    {
        Debug.Log("highlight");
        if (highlight)
            spriteRenderer.color = Color.green;
        else
            spriteRenderer.color = Color.white;
    }
}
