using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public Camera mainCamera;
    public Camera minimapCamera;
    
    private int tileX;
    private int tileY;

    private void Start()
    {
        tileX = 0;
        tileY = 0;

        MoveMainCamera(Board.boardInstance.tiles[tileX, tileY].gridPosition);
    }

    // Input arrow keys to move the camera for Tile position
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && tileY < Board.boardInstance.tiles.GetLength(1) - 1)
        {
            MiniMap.instance.miniMapTiles[tileX, tileY].GetComponent<MiniMapTile>().UnhighlightTile();
            MoveMainCamera(Board.boardInstance.tiles[tileX, tileY + 1].gridPosition);
            tileY++;
            MiniMap.instance.miniMapTiles[tileX, tileY].GetComponent<MiniMapTile>().HighlightTile();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && tileY > 0)
        {
            MiniMap.instance.miniMapTiles[tileX, tileY].GetComponent<MiniMapTile>().UnhighlightTile();
            MoveMainCamera(Board.boardInstance.tiles[tileX, tileY - 1].gridPosition);
            tileY--;
            MiniMap.instance.miniMapTiles[tileX, tileY].GetComponent<MiniMapTile>().HighlightTile();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && tileX > 0)
        {
            MiniMap.instance.miniMapTiles[tileX, tileY].GetComponent<MiniMapTile>().UnhighlightTile();
            MoveMainCamera(Board.boardInstance.tiles[tileX - 1, tileY].gridPosition);
            tileX--;
            MiniMap.instance.miniMapTiles[tileX, tileY].GetComponent<MiniMapTile>().HighlightTile();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && tileX < Board.boardInstance.tiles.GetLength(0) - 1)
        {
            MiniMap.instance.miniMapTiles[tileX, tileY].GetComponent<MiniMapTile>().UnhighlightTile();
            MoveMainCamera(Board.boardInstance.tiles[tileX + 1, tileY].gridPosition);
            tileX++;
            MiniMap.instance.miniMapTiles[tileX, tileY].GetComponent<MiniMapTile>().HighlightTile();
        }
    }


    public void MoveMainCamera(Vector2Int targetPosition)
    {
      

        mainCamera.transform.position = new Vector3(targetPosition.x, targetPosition.y, -1);

       
    }
}
