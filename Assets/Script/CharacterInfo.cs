using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour {
	public Character character;
	[SerializeField] GameManager gameManager;
	[SerializeField] Camera camera;
	[SerializeField] Image HP;
	[SerializeField] Image mood;
	[SerializeField] Image hunger;
	[SerializeField] Text physique;
	[SerializeField] Text EQ;
	[SerializeField] Text textName;
	[SerializeField] Dropdown state;
	public void Close()
	{
		gameObject.SetActive(false);
	}

	public void SetState(int value)
	{
		Crew crew = (Crew)character;
		crew.SetState(value);
	}

	public void Follow()
	{
		Crew crew = (Crew)character;
		crew.state = Crew.FOLLOWING;
	}

	public void UpdateCharacterCamera()
	{
		camera.transform.SetParent(character.transform);
		camera.transform.localPosition = new Vector3(0, 1, 1);
		camera.transform.localRotation = Quaternion.Euler(0, 180, 0);
	}
	void UpdateSlider()
	{
		HP.fillAmount = (float)character.hp / character.maxHp;
		mood.fillAmount = (float)character.mood / character.maxMood;
		hunger.fillAmount = (float)character.hunger / character.maxHunger;
	}

	void UpdateState()
	{
		if (character.tag == "Pilot")
		{
			state.gameObject.SetActive(false);
		}
		else if (character.tag == "Crew")
		{
			state.gameObject.SetActive(true);
			Crew crew = (Crew)character;
			state.value = crew.state;
		}
		
	}

	void UpdateInfo()
	{
		textName.text = character.characterName;
		physique.text = character.GetPhysique();
		EQ.text = character.EQ.ToString();
	}

	public void OnActive()
	{
		textName.text = character.characterName;
		if (character.tag == "Pilot")
		{
			state.gameObject.SetActive(false);
		}
		else if (character.tag == "Crew")
		{
			state.gameObject.SetActive(true);
		}
	}

	private void OnDisable()
	{
		UnblockOperator();
	}
	// Use this for initialization
	void Start () {
		gameManager = GameManager.Instance();
	}
	private void OnEnable()
	{
		BlockOperator();
	}

	void BlockOperator()
	{
		gameManager.opreateLock = true;
	}

	void UnblockOperator()
	{
		gameManager.opreateLock = false;
	}
	// Update is called once per frame
	void Update () {
		UpdateSlider();
		UpdateCharacterCamera();
		UpdateInfo();
	}
}
