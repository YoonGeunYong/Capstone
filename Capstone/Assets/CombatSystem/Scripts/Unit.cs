using UnityEngine;
using System.Collections.Generic;


public class Unit : MonoBehaviour
{
    public Vector2Int boardPosition;

    public void MoveTo(Vector2Int newBoardPosition)
    {

        gameObject.transform.position = new Vector3(newBoardPosition.x, 0, newBoardPosition.y);

        Debug.Log("Unit moved to " + newBoardPosition);

        boardPosition = newBoardPosition;

    }

    public Vector2Int GetBoardPosition()
    {
        return boardPosition;
    }
}
