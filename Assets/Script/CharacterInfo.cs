using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour {
	public Character character;
	[SerializeField] Camera camera;
	[SerializeField] Image HP;
	[SerializeField] Image mood;
	[SerializeField] Image hunger;
	[SerializeField] Text physique;
	[SerializeField] Text EQ;
	public void Close()
	{
		gameObject.SetActive(false);
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

	void UpdateInfo()
	{
		physique.text = character.GetPhysique();
		EQ.text = character.EQ.ToString();
	}
	// Use this for initialization
	void Start () {
		
	}

	private void OnEnable()
	{
	}
	// Update is called once per frame
	void Update () {
		UpdateSlider();
		UpdateCharacterCamera();
		UpdateInfo();
	}
}
