using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	public int hp;
	public int mood;
	public int hunger;
	public int EQ;
	public int physique;
	public int maxHp;
	public int maxMood;
	public int maxHunger;
	const int maxEQ = 10;
	const int NORMAL = 0;
	const int STRONG = 1;
	const int FAT = 2;

	public string GetPhysique()
	{
		switch (physique)
		{
			case 0: return "正常";
			case 1: return "强壮";
			case 2: return "肥胖";
			default:return "未知";
		}
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
