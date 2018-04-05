using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	// Component
	UnityEngine.AI.NavMeshAgent agent;
	Animator					animator;

	// Use this for initialization
	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0))
		{
			Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			Debug.DrawRay(mouseRay.origin, mouseRay.direction, Color.red);
			if (Physics.Raycast(mouseRay, out hit))
			{
				agent.SetDestination(hit.point);
			}
		}
		if (agent.velocity.magnitude >= 0.2f)
			animator.SetBool("run", true);
		else
			animator.SetBool("run", false);
	}
}
