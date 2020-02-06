using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
	public int time;
	public int day;
	public Light light;
	public bool state;
	public int weather;
	public Crew[] crews;
	public Pilot pilot;
	public bool opreateLock;
	public CharacterInfo characterInfo;
	public const int SUNNY = 0;
	public const int RAINDY = 1;
	public const int TIME_COUNT = 10;
	[SerializeField] TextMeshProUGUI timeText;
	private static GameManager instance;
	public static GameManager Instance()
	{
		return instance;
	}

	private IEnumerator Timer()
	{
		while (time >= 0)
		{
			timeText.text = time.ToString();
			yield return new WaitForSeconds(1);
			--time;
			if (time < 7 && time >= 0)
			{
				if (state == true)
				{
					light.intensity -= 0.1f;
				}
				else
				{
					light.intensity += 0.1f;
				}
			}
		}

	}


	private void DayToNight()
	{
		state = !state;
		time = TIME_COUNT;
		StartCoroutine(Timer());
		if (state == true)
		{
			light.intensity = 0.7f;
		}
		else
		{
			light.intensity = 0;
		}
	}

	private void DayAndNightRecycle()
	{
		if(time < 0)
		{
			DayToNight();
		}
	}

	private void Start()
	{
		state = true;
		time = TIME_COUNT;
		StartCoroutine(Timer());
		instance = this;
		opreateLock = false;
	}

}
