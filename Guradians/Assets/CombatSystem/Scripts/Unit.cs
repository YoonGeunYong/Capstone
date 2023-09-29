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
}
