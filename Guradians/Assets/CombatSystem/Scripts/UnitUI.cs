using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUi : MonoBehaviour 
{
     public Unit unit;  // Reference to the associated unit

     private int minimapWidth;
     private int minimapHeight;

    private void Start()
    {
        RectTransform minimapRect = MinimapManager.instance.minimapRect;

        // Assuming minimapRect is already assigned in the inspector
        minimapWidth = (int)minimapRect.rect.width;
        minimapHeight = (int)minimapRect.rect.height;

        UpdateMinimapPosition();
    }

    public void UpdateMinimapPosition()
    {
        Vector3 minimapPos = ConvertToMinimapCoordinates(unit.gridPosition);

	    transform.localPosition=minimapPos;
	    Debug.Log("Minimao position: "+ minimapPos);
    }

    private Vector3 ConvertToMinimapCoordinates(Vector2Int boardPos)
    {
        float adjustedX=boardPos.x*4f-minimapWidth/2f+4f/2f; 
	    float adjustedY=boardPos.y*4f-minimapHeight/2f+4f/2f; 

	    return new Vector3(adjustedX,adjustedY,0);
    }
}
