using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    
    private Dictionary<GameObject, GameObject> unitToUIMap = new Dictionary<GameObject, GameObject>();

    public MiniMapTile[,] miniMapTiles;
    public GameObject           miniMapTilePrefab;  // Assign this in the inspector.

    public void InitMiniMap(int width, int height)
    {
        miniMapTiles = new MiniMapTile[width, height];


        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {

                int adjustedX                     =   (x * 10) + 100; // Adjust these values according to your needs.
                int adjustedY                     =   (y * 10) + 100;
                

                int minimapGridPositionX          =   adjustedX - 100;
                int minimapGridPositionY          =   adjustedY - 100;

                
                MiniMapTile tileComponent         =   miniMapTilePrefab.GetComponent<MiniMapTile>();
                tileComponent.gridPosition        =   new Vector2Int(minimapGridPositionX, minimapGridPositionY);
                miniMapTiles[x, y]                =   tileComponent;


                Instantiate(miniMapTilePrefab, new Vector3(adjustedX, 0, adjustedY), Quaternion.identity);
            }


    }

    public void AddUnitToMinimap(Unit unit, Vector2Int positionOnBoard)
    {
        var minimapIcon = Instantiate(unit.unitUIPrefab);
        minimapIcon.transform.SetParent(transform);

        var minimapPos = ConvertBoardPosToMinimapPos(positionOnBoard);
        minimapIcon.transform.position = new Vector3(minimapPos.x, 0, minimapPos.y);

        unitToUIMap[unit.unitPrefab] = minimapIcon;
    }

    private Vector2Int ConvertBoardPosToMinimapPos(Vector2Int boardPos)
    {
        // Implement this function based on how your board and minimap are related.
        throw new NotImplementedException();
    }

    public void UpdateUnitOnMinimap(GameObject unitObject, Vector2Int newPositionOnBoard)
    {
        if (unitToUIMap.TryGetValue(unitObject, out GameObject minimapIcon))
        {
            var minimapPos = ConvertBoardPosToMinimapPos(newPositionOnBoard);
            minimapIcon.transform.position = new Vector3(minimapPos.x, 0, minimapPos.y);
        }
        else
        {
            Debug.LogError("No UI found for this unit on the minimap.");
        }
    }
}