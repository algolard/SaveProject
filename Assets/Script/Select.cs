using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour {
	public Material material;
	Character character;
	// Use this for initialization
	void Start () {
		material = GetComponent<Renderer>().material;
		character = GetComponentInParent<Character>();
	}

	private void IsSelected()
	{
		if (character.selected)
		{
			material.shader = Shader.Find("Custom/OutLine");
		}
		else
		{
			material.shader = Shader.Find("Standard");
		}
	}

	// Update is called once per frame
	void Update () {
		IsSelected();
	}
}
