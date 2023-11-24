using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int   gridPosition;
    public Board        board;
    public float        noiseScale = 100f; // The scale of the Perlin noise.
    public int         isCostTile = 0;

    public Material     grass1;
    public Material     grass2;
    public Material     grass3;


    public void ApplyNatureTexture()
    {

        float noiseValue = GeneratePerlinNoise(gridPosition.x, gridPosition.y, noiseScale);

        if (noiseValue < 0.3f)
        {
            GetComponent<Renderer>().material = grass1;
            this.gameObject.tag = "CostTile";
            for (int i = 0; i < 3; i++)
            {
                if (GameController.instance.tile[i] is not null) continue;
                GameController.instance.tile[i] = this;
                break;

            }
        }

        else if (noiseValue < 0.6f)
        {
            GetComponent<Renderer>().material = grass2;
        }

        else
        {
            GetComponent<Renderer>().material = grass3; 
        }

    }

    private float GeneratePerlinNoise(int x, int y, float scale)
    {

        float xCoord    = (float)x / 256 * scale;
        float yCoord    = (float)y / 256 * scale;

        return Mathf.PerlinNoise(xCoord, yCoord);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CompareTag("CostTile"))
        {
            isCostTile = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (CompareTag("CostTile"))
        {
            isCostTile = 0;
        }
    }
}
