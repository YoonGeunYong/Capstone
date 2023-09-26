using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    // Position on the board.
    private Vector2Int position;


    public void Init(Vector2Int position)
    {
        this.position = position;
    }

    // This method creates a new unit at the base's position.
    public Unit SpawnUnit(GameObject unitPrefab)
    {
        GameObject newUnitObject = Instantiate(unitPrefab, new Vector3(position.x, 0, position.y), Quaternion.identity);
        Unit newUnit = newUnitObject.GetComponent<Unit>();

        // Set the initial properties of the unit...

        return newUnit;
    }
}
