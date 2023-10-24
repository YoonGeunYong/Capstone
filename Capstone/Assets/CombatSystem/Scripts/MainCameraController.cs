using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public void MoveTo(Vector3 newPosition)
    {
        // Here you can implement the logic to move the camera.
        // This is a simple example where we just set the new position directly.
        transform.position = newPosition;
    }
}
