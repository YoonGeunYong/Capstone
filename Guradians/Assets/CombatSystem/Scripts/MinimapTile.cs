using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapTile : MonoBehaviour
{
    public UnitUI UnitUIOnTop { get; set; }  // The unit UI on this tile.

    private void OnMouseDown()
    {
        // This method is called when the mouse button is pressed over the collider of this game object.

        MainCameraController mainCamera = FindObjectOfType<MainCameraController>();

        if (mainCamera != null)
            mainCamera.MoveTo(transform.position);

        /* For more complex games you might need to adjust Z position or use some kind of offset */

        /* Vector3 newPosition= new Vector3(transform.position.x,transform.position.y,someZvalue); 
           mainCamera.MoveTo(newPosition);
         */


    }
}
