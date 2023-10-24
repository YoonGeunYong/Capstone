using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapTile : MonoBehaviour
{
    public Vector2Int gridPosition;
    public Vector2Int boardPosition;
    public bool IsMovable { get; set; }  // Whether this tile is currently movable.

    /*
    private void OnMouseDown()
    {

        //MainCameraController mainCamera = FindObjectOfType<MainCameraController>();

        //if (mainCamera != null)
        //    mainCamera.MoveTo(transform.position);
        if(IsMovable)
        {
            //GameController.instance.MoveUnitTo(boardPosition);

            if (MiniMap.instance != null)
            {
                MiniMap.instance.MoveUnitTo(this);
                IsMovable = false;
            }
                

            Debug.Log("Tile clicked!");
        } 
    }
    */
}
