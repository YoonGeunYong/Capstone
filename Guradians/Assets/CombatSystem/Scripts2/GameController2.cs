using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController2 : MonoBehaviour
{
    // ...

    public Board2 gameBoard;
    public Minimap minimap;

    private void Awake()
    {
        // Initialize the game board and the minimap...
        Vector2Int basePos = gameBoard.Init(width, height);
        playerBase.Init(basePos);

        if (minimap != null)
            minimap.Init(gameBoard);

        // ...
    }

    // Other methods...
}
