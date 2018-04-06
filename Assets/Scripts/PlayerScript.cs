using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	// Component
	UnityEngine.AI.NavMeshAgent agent;
	Animator					animator;
	bool						attack;

	// Use this for initialization
	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (animator.GetBool("attack") == false) // Peu se deplacer et/ou attaquer si il n'y a pas d'annimation d'attack en cour
		{
			if (Input.GetMouseButton(0))
			{
				Move();
				attack = false;
			}
			if (Input.GetMouseButtonDown(1))
			{
				Move();
				attack = true;
			}
		}
		// Debug.Log("Velocity: " + agent.velocity.magnitude);
		bool endMove = pathComplete();
		animator.SetBool("run", !endMove);
		if (endMove && attack && agent.velocity.magnitude <= 0.0f)
		{
			animator.SetTrigger("attack");
			attack = false;
		}
	}

	void Move()
	{
		Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		Debug.DrawRay(mouseRay.origin, mouseRay.direction, Color.red);
		if (Physics.Raycast(mouseRay, out hit))
		{
			agent.SetDestination(hit.point);
		}
	}
	protected bool pathComplete()
	{
		if (Vector3.Distance(agent.destination, agent.transform.position) <= agent.stoppingDistance)
		{
			if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
			{
				return (true);
			}
		}
		return (false);
	}
}
