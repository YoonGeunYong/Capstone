using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCameraController : MonoBehaviour
{
    public Camera miniMapCamera;

    public void UpdateCameraSize(MiniMap miniMap)
    {

        // Adjust the camera's position to the center of the minimap.
        float centerX                    = (miniMap.width * 9) / 2f + 100;
        float centerY                    = (miniMap.height * 9) / 2f + 100;


        miniMapCamera.transform.position = new Vector3(centerX, centerY, miniMapCamera.transform.position.z);

        // Adjust the camera's orthographicSize to cover the whole minimap.
        int mapSize = Mathf.Max(miniMap.width, miniMap.height) * 10;

        miniMapCamera.orthographicSize = mapSize/2;

    }
}
