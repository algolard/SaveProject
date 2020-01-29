using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	[SerializeField] int hp;
	[SerializeField] int mood;
	[SerializeField] int hunger;
	[SerializeField] int EQ;
	[SerializeField] int physique;
	int maxHp;
	int maxMood;
	int maxHunger;
	const int maxEQ = 10;
	const int NORMAL = 0;
	const int STRONG = 1;
	const int FAT = 2;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
