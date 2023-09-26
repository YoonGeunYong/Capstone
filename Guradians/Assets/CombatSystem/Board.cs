using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private int width;
    private int height;

    // 2D array representing the board tiles.
    private Tile[,] tiles;

    public Base playerBase;
    public Base enemyBase;

    private void Start()
    {
        playerBase = GetComponent<Base>();
        enemyBase = GetComponent<Base>();
    }

    public void InitBase(int width, int height)
    {
        this.width = width;
        this.height = height;

        // Instantiate bases at the corners of the board.
        playerBase.Init(new Vector2Int(0, 0));
        enemyBase.Init(new Vector2Int(width - 1, height - 1));
    }

    public void InitBoard(int width, int height)
    {
        this.width = width;
        this.height = height;

        //tiles = new Unit[width, height];
    }

    // Add methods for moving units around the board here...
}
