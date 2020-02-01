using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	RaycastHit hit;
	public LayerMask clickableLayer;
	[SerializeField] CharacterInfo characterInfo;
	void MouseClick()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Character character;
		if (Physics.Raycast(ray, out hit, 50, clickableLayer.value))
		{
			print(hit.collider.name);
			character = hit.collider.GetComponent<Character>();
			characterInfo.gameObject.SetActive(true);
			characterInfo.character = character;
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			MouseClick();
		}
	}
}
