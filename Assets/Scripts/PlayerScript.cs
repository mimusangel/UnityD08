using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	// Component
	UnityEngine.AI.NavMeshAgent agent;
	Animator					animator;
	bool						attack;

	GameObject					target;

	Vector3						origin;

	// Use this for initialization
	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		animator = GetComponentInChildren<Animator>();
		origin = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (animator.GetBool("attack") == false && !HUDScript.MenuIsOpen) // Peu se deplacer et/ou attaquer si il n'y a pas d'annimation d'attack en cour
		{
			if (Input.GetMouseButton(0))
			{
				Move();
				attack = false;
			}
			if (Input.GetMouseButton(1))
			{
				if (target && target.tag == "Enemy")
					agent.SetDestination(target.transform.position);
				
				else
					Move();
				attack = true;
			}
		}
		// Debug.Log("Velocity: " + agent.velocity.magnitude);
		bool endMove = pathComplete();
		animator.SetBool("run", !endMove);
		if (endMove && attack && agent.velocity.magnitude <= 0.0f)
		{
			transform.rotation = Quaternion.LookRotation((target.transform.position - transform.position).normalized);
			animator.SetTrigger("attack");
			attack = false;
			CharacterScript cs = GetComponent<CharacterScript>();
			CharacterScript ocs = getTarget();
			if (cs && ocs)
				ocs.takeDamage(cs);
		}
	}

	void Move()
	{
		RaycastHit hit;
		if (raycastHit(out hit))
		{
			agent.SetDestination(hit.point);
			target = hit.collider.gameObject;
		}
	}

	bool raycastHit(out RaycastHit hitout)
	{
		Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll(mouseRay);
		Debug.DrawRay(mouseRay.origin, mouseRay.direction, Color.red);
		foreach(RaycastHit hit in hits)
		{
			if (hit.collider.gameObject.tag == "Enemy")
			{
				hitout = hit;
				return (true);
			}
		}
		foreach(RaycastHit hit in hits)
		{
			if (hit.collider.gameObject.tag != "Player")
			{
				hitout = hit;
				return (true);
			}
		}
		hitout = new RaycastHit();
		return (false);
	}

	protected bool pathComplete()
	{
		if ((agent.transform.position - agent.destination).sqrMagnitude <= agent.stoppingDistance * agent.stoppingDistance)
		{
			if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
			{
				return (true);
			}
		}
		return (false);
	}

	public void death()
	{
		transform.position = origin;
		agent.SetDestination(origin);
	}

	public CharacterScript		getTarget()
	{
		if (!target)
			return (null);
		CharacterScript ocs = target.GetComponentInChildren<CharacterScript>();
		if (ocs)
			return (ocs);
		return (null);
	}
}
