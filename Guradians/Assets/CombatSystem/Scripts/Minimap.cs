using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public GameObject           miniMapTilePrefab;  // Assign this in the inspector.

    private MiniMapTile[,]      miniMapTiles;

    public void InitMiniMap(int width, int height)
    {
        miniMapTiles = new MiniMapTile[width, height];


        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                float adjustedX                     =   (x * 10f) + 100f; // Adjust these values according to your needs.
                float adjustedY                     =   (y * 10f) + 100f;


                GameObject tileObject               =   Instantiate(miniMapTilePrefab, new Vector3(adjustedX, 0, adjustedY), Quaternion.identity);
                MiniMapTile tileComponent           =   tileObject.GetComponent<MiniMapTile>();


                miniMapTiles[x / width, y / height] =   tileComponent;
            }
    }

    public void UpdateUnitUIPosition(Unit unit)
    {


    }

}