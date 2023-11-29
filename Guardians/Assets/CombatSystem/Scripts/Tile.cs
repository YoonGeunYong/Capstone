using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int       gridPosition;
    public Board            board;
    public float            noiseScale;

    public Material         grass1;
    public Material         grass2;
    public Material         grass3;

    private void Start()
    {
            
       noiseScale = 100f;

    }


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
        }

        else
        {
            GetComponent<Renderer>().material = grass3;
        }

    }


    private float GeneratePerlinNoise(int x, int y, float scale)
    {

        float xCoord = (float)x / 256 * scale;
        float yCoord = (float)y / 256 * scale;

        return Mathf.PerlinNoise(xCoord, yCoord);

    }
}