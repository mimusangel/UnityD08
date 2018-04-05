using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraScript : MonoBehaviour {

	GameObject	player;
	Vector2		angle;
	float		distance = 10.0f;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		angle = new Vector2(45, -45);
		//Quaternion.AngleAxis(angle.x, Vector3.right);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.R))
		{
			Cursor.lockState = CursorLockMode.Locked;
			angle.y += Input.GetAxis("Mouse X");
			angle.x += Input.GetAxis("Mouse Y");
			angle.x = Mathf.Clamp(angle.x, 20.0f, 89.0f);
		}

		distance += Input.GetAxis("Mouse ScrollWheel");
		distance = Mathf.Clamp(distance, 6.0f, 15.0f);

		if (Input.GetKeyUp(KeyCode.R))
			Cursor.lockState = CursorLockMode.None;

		transform.rotation = Quaternion.Euler(angle.x, angle.y, 0);

		RaycastHit hit;
		if(Physics.Raycast(player.transform.position, transform.forward * -1, out hit, distance))
			transform.position = hit.point;
		else
			transform.position = player.transform.position + transform.forward * -distance;

	}
}
