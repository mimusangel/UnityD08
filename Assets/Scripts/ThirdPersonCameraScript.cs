using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraScript : MonoBehaviour {

	List<GameObject> listInRay;
	GameObject	player;
	Vector2		angle;
	float		distance = 10.0f;

	// Use this for initialization
	void Start () {
		listInRay = new List<GameObject>();
		player = GameObject.FindGameObjectWithTag("Player");
		angle = new Vector2(45, -45);
		transform.rotation = Quaternion.Euler(angle.x, angle.y, 0);
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
			transform.rotation = Quaternion.Euler(angle.x, angle.y, 0);
		}
		distance += Input.GetAxis("Mouse ScrollWheel");
		distance = Mathf.Clamp(distance, 6.0f, 15.0f);
		transform.position = player.transform.position + transform.forward * -distance;
		if (Input.GetKeyUp(KeyCode.R))
			Cursor.lockState = CursorLockMode.None;

		clearGameObjectInList();
		addGameObjectInList();
	}

	void clearGameObjectInList()
	{
		foreach(GameObject obj in listInRay)
			obj.SetActive(true);
		listInRay.Clear();
	}

	void addGameObjectInList()
	{
		RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, (player.transform.position - transform.position).magnitude);
		foreach(RaycastHit hit in hits)
		{
			if (hit.collider.gameObject.tag == "Player" || hit.collider.gameObject.tag == "Enemy")
				continue ;
			hit.collider.gameObject.SetActive(false);
			listInRay.Add(hit.collider.gameObject);
		}
	}
}
