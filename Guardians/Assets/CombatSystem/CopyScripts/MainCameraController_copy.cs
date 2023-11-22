using UnityEngine;

public class MainCameraController_copy : MonoBehaviour
{
    public      Camera  mainCamera;
    public      Camera  minimapCamera;
    private     float   lastClickTime           = 0f;
    private     float   doubleClickThreshold    = 0.25f; 


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { 
            if (Time.time - lastClickTime < doubleClickThreshold)
            {

                RaycastHit hit;
                Ray        ray = minimapCamera.ScreenPointToRay(Input.mousePosition);


                if (Physics.Raycast(ray, out hit))
                {

                    MiniMapTile_copy miniMapTile = hit.transform.GetComponent<MiniMapTile_copy>();

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

