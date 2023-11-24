using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public      Camera  mainCamera;
    public      Camera  minimapCamera;
    private     float   lastClickTime           = 0f;
    private     float   doubleClickThreshold    = 0.25f;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            if (Time.time - lastClickTime < doubleClickThreshold)
            {

                RaycastHit2D hit;
                Vector2        ray = minimapCamera.ScreenToWorldPoint(Input.mousePosition);
                hit = Physics2D.Raycast(ray, Vector2.zero);

                if (hit.collider != null)
                {
                    MiniMapTile miniMapTile = hit.transform.GetComponent<MiniMapTile>();

                    if (miniMapTile != null)
                    {

                        Vector3 mapPosition = new Vector3(miniMapTile.gridPosition.x - 100, miniMapTile.gridPosition.y - 100, mainCamera.transform.position.z);
                        MoveMainCamera(mapPosition);

                    }

                }
            }
            lastClickTime = Time.time;
        }
    }

    public void MoveMainCamera(Vector3 targetPosition)
    {
        // Change only the position of the camera.
        mainCamera.transform.position = targetPosition;
    }
}

