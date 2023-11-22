using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_copy : MonoBehaviour
{
    public Vector2Int   gridPosition;
    public Board_copy        board;
    public float        noiseScale = 100f; // The scale of the Perlin noise.

    public Material     grass1;
    public Material     grass2;
    public Material     grass3;


    public void ApplyNatureTexture()
    {

        float noiseValue = GeneratePerlinNoise(gridPosition.x, gridPosition.y, noiseScale);

        if (noiseValue < 0.3f)
        {
            GetComponent<Renderer>().material = grass1;
        }

        else if (noiseValue < 0.6f)
        {
            GetComponent<Renderer>().material = grass2;
            this.gameObject.tag = "CostTile";
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collise");
    }
}
