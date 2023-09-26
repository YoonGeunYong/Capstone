using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    // Position on the board.
    private Vector2Int basePosition;


    public void Init(Vector2Int position)
    {
        basePosition = position;
        Debug.Log("Base initialized at " + position);
    }

    // This method creates a new unit at the base's position.
    public Unit SpawnUnit(GameObject unitPrefab)
    {
        GameObject newUnitObject = Instantiate(unitPrefab, new Vector3(basePosition.x, 0, basePosition.y), Quaternion.identity);
        Unit newUnit = newUnitObject.GetComponent<Unit>();

        // Set the initial properties of the unit...

        return newUnit;
    }

    public Vector2Int GetPosition()
    {
        return basePosition;
    }
}
