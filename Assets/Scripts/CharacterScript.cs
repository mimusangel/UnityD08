using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour {

	public int level = 1;
	public float experience = 0;
	public float life;

	public int strength = 5;
	public int dexterity = 5;
	public int vitality = 5;
	public int energy = 5;
	public int points = 0;

	public float lastDamageTime = -5.0f;

	public float lastChanceToHit = 100.0f;

	// Use this for initialization
	void Start () {
		life = getMaxLife();
	}

	public void RandomStat(int lvl)
	{
		level = lvl;
		int min = (level - 2) * 5;
		min = Mathf.Max(min, 1);
		strength = Random.Range(min, level * 5 + level);
		dexterity = Random.Range(min, level * 5 + level);
		vitality = Random.Range(min, level * 5 + level);
		energy = Random.Range(min, level * 5 + level);
		life = getMaxLife();
	}
	
	private void Update() {
		if (Time.time - lastDamageTime > 10.0f)
		{
			life += getMaxLife() * 0.1f * Time.deltaTime;
			life = Mathf.Min(life, getMaxLife());
		}
	}

	public float getMaxLife()
	{
		return (45.0f + level * 5.0f + vitality * 10.0f);
	}

	public int getDamage()
	{
		return (5 + level * 2 + strength * 5);
	}

	public int getDefence()
	{
		return (2 + level * 2 + dexterity * 5);
	}

	public int getNextLevel()
	{
		float lvl = (float)level;
		float calc = 200.0f * lvl + 200.0f * lvl * (lvl / 100.0f);
		return Mathf.RoundToInt(calc);
	}
	public int getLastNextLevel()
	{
		if (level <= 1)
			return (0);
		float lvl = (float)level - 1.0f;
		float calc = 200.0f * lvl + 200.0f * lvl * (lvl / 100.0f);
		return Mathf.RoundToInt(calc);
	}

	public float getChanceToHit(CharacterScript other)
	{
		lastChanceToHit = (100.0f * ((float)getDamage() / (float)(getDamage() + other.getDefence())) * ((float)level / (float)(level + other.level)));
		return (lastChanceToHit);
	}

	public void takeDamage(CharacterScript other)
	{
		lastDamageTime = Time.time;
		float chance = other.getChanceToHit(this);
		if (Random.Range(0.0f, 100.0f) <= chance)
		{
			float rngDamage = other.getDamage() + Random.Range(-other.getDamage() / 2, other.getDamage() / 2);
			float damage = rngDamage - getDefence();
			damage = Mathf.Max(damage, 1);
			life = Mathf.Max(life - damage, 0);
			if (life <= 0)
			{
				if (gameObject.tag == "Player")
				{
					gameObject.GetComponent<PlayerScript>().death();
					life = getMaxLife();
					experience = Mathf.RoundToInt((float)experience * 0.95f);
					return ;
				}
				else
				{
					float rate = ((float)level / (float)(level + other.level));
					other.takeExperience((float)(50.0f + level * 10.0f + level / 2.0f * 10.0f) * rate);
					Destroy(transform.parent.gameObject);
				}
			}
			Debug.Log(name + " take damage by " + other.name);
			Debug.Log(name + ": " + life + " / " + getMaxLife());
		}
		else
			Debug.Log(name + " esquive damage!");
	}

	public void takeExperience(float exp)
	{
		exp = Random.Range(exp * 0.9f, exp * 1.1f);
		Debug.Log(name + " gain " + exp + "exp!");
		experience += exp;
		if (experience >= getNextLevel())
		{
			level++;
			points += 5;
			Debug.Log(name + " Level UP!");
		}
	}

}
