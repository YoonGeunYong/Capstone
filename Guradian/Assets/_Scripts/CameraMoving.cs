using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    //float horizontal;
    //float vertical;
    public int speed;

    //Vector3 position;


	void Start()
    {
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

		Vector3 position = transform.position;

        position.x += horizontal * speed * Time.deltaTime;
		position.y += vertical * speed * Time.deltaTime;

        transform.position = position;
	}
}
