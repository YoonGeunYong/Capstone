using UnityEngine;

public class GameController2 : MonoBehaviour
{
    public static GameController2 instance;

    public Board2 gameBoard;
    public Minimap minimap;

    // Define the width and height of the game board.
    public int width;
    public int height;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.LogError("Another instance of GameController already exists!");
            Destroy(this);
            return;
        }

        // Initialize the game board and the minimap...
        Vector2Int basePos = gameBoard.Init(width, height);

        if (minimap != null)
            minimap.Init(gameBoard);

        // ...
    }

    // Other methods...
}
