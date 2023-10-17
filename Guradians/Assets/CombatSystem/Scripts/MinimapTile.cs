using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapTile : MonoBehaviour
{
    public Vector2Int gridPosition;
    public UnitUI UnitUIOnTop { get; set; }  // The unit UI on this tile.

    private void OnMouseDown()
    {

        MainCameraController mainCamera = FindObjectOfType<MainCameraController>();

        if (mainCamera != null)
            mainCamera.MoveTo(transform.position);
    }
}
