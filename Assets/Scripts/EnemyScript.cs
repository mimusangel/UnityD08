using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	public UnityEngine.AI.NavMeshAgent agent;
	public Animator					animator;
	bool						attack;
	GameObject					target;

	float						cooldownAttack = 0.0f;
	Vector3						originPos;
	// Use this for initialization
	void Start () {
		originPos = agent.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		cooldownAttack -= Time.deltaTime;
		if (cooldownAttack <= 0.0f)
		{
			if (target && target.tag == "Player")
			{
				agent.SetDestination(target.transform.position);
				attack = true;
			}
		}
		bool endMove = pathComplete();
		animator.SetBool("run", !endMove);
		if (endMove && attack && agent.velocity.magnitude <= 0.0f)
		{
			transform.rotation = Quaternion.LookRotation((target.transform.position - transform.position).normalized);
			animator.SetTrigger("attack");
			attack = false;
			cooldownAttack = 1.0f;
			CharacterScript cs = GetComponent<CharacterScript>();
			CharacterScript ocs = target.GetComponent<CharacterScript>();
			if (cs && ocs)
				ocs.takeDamage(cs);
		}
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

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player")
		{
			target = other.gameObject;
			agent.SetDestination(target.transform.position);
			attack = true;
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Player")
		{
			target = null;
			attack = false;
			agent.SetDestination(originPos);
		}
	}
}
