using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitUi : MonoBehaviour, IPointerClickHandler
{
    public Unit unit;  // Reference to the associated unit
    public GameObject borderGlow;  // Assign this in the inspector.

    private int minimapWidth;
    private int minimapHeight;

    private void Start()
    {
        RectTransform minimapRect = MinimapManager.instance.minimapRect;

        // Assuming minimapRect is already assigned in the inspector
        minimapWidth = (int)minimapRect.rect.width;
        minimapHeight = (int)minimapRect.rect.height;
        borderGlow.SetActive(false);

        //UpdateMinimapPosition();
    }

    public void UpdateMinimapPosition()
    {
        Vector3 minimapPos = ConvertToMinimapCoordinates(unit.gridPosition);

	    transform.localPosition=minimapPos;
	    Debug.Log("Minimao position: "+ minimapPos);
    }

    private Vector3 ConvertToMinimapCoordinates(Vector2Int boardPos)
    {
        float adjustedX = boardPos.x * 4f; 
	    float adjustedY = boardPos.y * 4f; 

	    return new Vector3(adjustedX,adjustedY,0);
    }

    public void ShowPossibleMoves()
    {
        List<Vector2Int> moves = unit.GetPossibleMoves();
        
        foreach (Vector2Int move in moves)
        {
            
            Tile tile = GameController.instance.board.GetTileAt(move);
            Debug.Log("Tile: "+tile);
            tile.Highlight(true);   // Assume Highlight method changes the tile color to green.
        }
        Debug.Log(moves.Count);
    }

    public void HidePossibleMoves()
    {
        List<Vector2Int> moves = unit.GetPossibleMoves();

        foreach (Vector2Int move in moves)
        {
            Tile tile = GameController.instance.board.GetTileAt(move);
            tile.Highlight(false);   // Assume Highlight method changes the tile color back to normal.
        }
    }

    public void Select()
    {
        borderGlow.SetActive(true);
    }

    public void Deselect()
    {
        borderGlow.SetActive(false);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!borderGlow.activeSelf)
            {
                Debug.Log("border");
                Select();
                ShowPossibleMoves();
            }
            else
            {
                Debug.Log("borderNo");
                Deselect();
                HidePossibleMoves();

                if (GameController.instance.board.selectedUnit == this.unit)
                    GameController.instance.board.DeselectCurrent();
            }
        } 
    }
}
