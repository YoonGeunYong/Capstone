using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public Camera mainCamera;
    public Camera minimapCamera;
    private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.25f; // Double click recognized within 0.25 seconds

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { // 0 for left click
            if (Time.time - lastClickTime < doubleClickThreshold)
            {
                RaycastHit hit;
                Ray ray = minimapCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Double click detected on minimap!");
                    MiniMapTile miniMapTile = hit.transform.GetComponent<MiniMapTile>();
                    if (miniMapTile != null)
                    {
                        Vector3 mapPosition = new Vector3(miniMapTile.gridPosition.x - 100, mainCamera.transform.position.y, miniMapTile.gridPosition.y - 100);
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

