using UnityEngine;
using System.Collections.Generic;


public class Unit : MonoBehaviour
{
    public Vector2Int boardPosition;
    public GameObject unitUI;

  
 

    public void MoveTo(Vector2Int newBoardPosition)
    {
        // Update unit's position in Unity world space.
        transform.position = new Vector3(newBoardPosition.x * 10, 0, newBoardPosition.y * 10);

        // Update unit's board position.
        boardPosition = newBoardPosition;

        // Notify the minimap of the movement.
        GameController.instance.miniMap.UpdateUnitOnMinimap(this.gameObject, boardPosition);
    }

    public Vector2Int GetBoardPosition()
    {
        return boardPosition;
    }
}
