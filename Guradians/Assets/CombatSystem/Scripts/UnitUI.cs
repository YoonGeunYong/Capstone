using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUI : MonoBehaviour
{
    public Unit LinkedUnit { get; set; }  // The unit this UI is linked to.
    public MiniMapTile CurrentTile { get; set; }  // The tile this UI is currently on.

    private void OnMouseDown()
    {
        // This method is called when the mouse button is pressed over the collider of this game object.
        Debug.Log("A unit UI is clicked.");
        // Here you can call some method to highlight possible movement tiles or to move the unit.
        //
        // Something like:
        //
        // LinkedUnit.OnClicked();
    }
}


