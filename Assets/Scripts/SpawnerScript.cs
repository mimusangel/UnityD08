using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

	public GameObject[]	spawnList;
	GameObject link = null;
	
	GameObject player;

	float cooldownSpawn = 2.0f;

	private void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update () {
		if (!link && spawnList.Length > 0)
		{
			cooldownSpawn -= Time.deltaTime;
			if (cooldownSpawn <= 0.0f)
			{
				link = GameObject.Instantiate(spawnList[Random.Range(0, spawnList.Length)], transform.position, transform.rotation);
				CharacterScript cs = link.GetComponentInChildren<CharacterScript>();
				if (cs)
				{
					CharacterScript pcs = player.GetComponent<CharacterScript>();
					if (pcs)
						cs.RandomStat(pcs.level);
				}
				cooldownSpawn = Random.Range(30.0f, 90.0f);
			}
		}
	}

	private void OnDrawGizmos() {
		Gizmos.DrawIcon(transform.position, "enemy.png");
	}
}
