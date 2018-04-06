using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {

	public static bool		MenuIsOpen { get; private set; }

	public PlayerScript		player;
	public CharacterScript	character;
	public Image			playerLifeImage;
	public Text				playerLifeText;
	public Image			playerExpImage;
	public Text				playerExpText;


	public GameObject		EnemyHUD;
	public Image			EnemyLifeBar;
	public Text				EnemyLifeText;
	public Text				EnemyLevelText;

	public GameObject		panelCharacter;
	public Text				cStrength;
	public Text				cDexterity;
	public Text				cVitality;
	public Text				cEnergy;
	public Text				cPoints;
	public Text				cDamage;
	public Text				cDefense;
	public Text				cChanceToHit;
	public Button			cBtnStrength;
	public Button			cBtnDexterity;
	public Button			cBtnVitality;
	public Button			cBtnEnergy;

	private void Start() {
		MenuIsOpen = panelCharacter.activeSelf;
	}

	// Update is called once per frame
	void Update () {
		playerLifeImage.fillAmount = character.life / character.getMaxLife();
		playerLifeText.text = Mathf.RoundToInt(character.life) + "/" + Mathf.RoundToInt(character.getMaxLife());
		float exp0 = (float)character.getLastNextLevel();
		float exp1 = (float)character.getNextLevel();
		float expPlayer = Mathf.Max((float)character.experience - exp0, 0.0f);
		playerExpImage.fillAmount = expPlayer / (exp1 - exp0);
		playerExpText.text = "EXP: " + Mathf.RoundToInt(character.experience) + "/" + Mathf.RoundToInt(exp1);
		// Target
		CharacterScript target = player.getTarget();
		if (target)
		{
			EnemyHUD.SetActive(true);
			EnemyLifeBar.fillAmount = target.life / target.getMaxLife();
			EnemyLifeText.text = Mathf.RoundToInt(target.life) + "/" + Mathf.RoundToInt(target.getMaxLife());
			EnemyLevelText.gameObject.SetActive(true);
			EnemyLevelText.text = "Level: " + target.level + " - " + target.strength + "/" + target.dexterity + "/" + target.vitality + "/" + target.energy;
		}
		else
		{
			EnemyHUD.SetActive(false);
			EnemyLevelText.gameObject.SetActive(false);
		}
		// Menu Character
		if (Input.GetKeyDown(KeyCode.C))
			panelCharacter.SetActive(!panelCharacter.activeSelf);
		if (panelCharacter.activeSelf)
		{
			cStrength.text = "Strength: " + character.strength;
			cDexterity.text = "Dexterity: " + character.dexterity;
			cVitality.text = "Vitality: " + character.vitality;
			cEnergy.text = "Energy: " + character.energy;
			cPoints.text = "Points: " + character.points;
			cBtnStrength.gameObject.SetActive(character.points > 0);
			cBtnDexterity.gameObject.SetActive(character.points > 0);
			cBtnVitality.gameObject.SetActive(character.points > 0);
			cBtnEnergy.gameObject.SetActive(character.points > 0);
			cDamage.text = "Damage: " + character.getDamage();
			cDefense.text = "Defense: " + character.getDefence();
			cChanceToHit.text = "Chance to Hit: " + Mathf.RoundToInt(character.lastChanceToHit) + "%";
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			closeCharacterPanel();
		}

		MenuIsOpen = panelCharacter.activeSelf;
	}

	public void UpStrength()
	{
		if (character.points > 0)
		{
			character.strength += 1;
			character.points -= 1;
		}
	}

	public void UpDexterity()
	{
		if (character.points > 0)
		{
			character.dexterity += 1;
			character.points -= 1;
		}
	}

	public void UpVitality()
	{
		if (character.points > 0)
		{
			character.vitality += 1;
			character.points -= 1;
		}
	}

	public void UpEnergy()
	{
		if (character.points > 0)
		{
			character.energy += 1;
			character.points -= 1;
		}
	}

	public void closeCharacterPanel()
	{
		panelCharacter.SetActive(false);
	}
}
