using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class Unit_copy : MonoBehaviour
{
    public Vector2Int boardPosition;

    public IEnumerator MoveTo(Vector2Int newBoardPos, float speed = 20f)
    {
        Vector3 targetPosition = new Vector3(newBoardPos.x * 10, transform.position.y, newBoardPos.y * 10);

        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPosition; // Ensure the unit reaches the exact target position
        boardPosition = newBoardPos; // Update the board position
    }
}
