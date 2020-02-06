using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operation : MonoBehaviour {
	GameManager gameManager;
	Vector3 startPosition, endPosition;
	Vector3 mousePosition;
	bool mouseDown;
	RaycastHit hit;
	public LayerMask clickableLayer;
	void ReadCharacterInfo()
	{
		CharacterInfo characterInfo = gameManager.characterInfo;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Character character;
		if (Physics.Raycast(ray, out hit, 50, clickableLayer.value))
		{
			print(hit.collider.name);
			character = hit.collider.GetComponent<Character>();
			characterInfo.gameObject.SetActive(true);
			characterInfo.character = character;
			characterInfo.OnActive();
		}
	}

	void SelectCharacter()
	{
		Crew[] crews = gameManager.crews;
		Vector3 worldPosition, screenPosition;
		mousePosition = Input.mousePosition;//记录鼠标位置
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Character character;
			mouseDown = true;//鼠标落下标记
			Debug.Log("mouseDown");
			startPosition = Input.mousePosition;//记录第一个点的位置
			if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
			{
				for (int i = 0; i < crews.Length; i++)
				{//将之前选择的物体全部释放
					crews[i].selected = false;
				}
			}
			if (Physics.Raycast(ray, out hit, 50, clickableLayer.value))
			{
				character = hit.collider.GetComponent<Character>();
				print(character.name);
				if (character.gameObject.tag == "Crew")
				{
					character.selected = true;
				}
				else if (character.gameObject.tag == "Pilot")
				{
					foreach (var crew in crews)
					{
						if (crew.selected == true)
						{
							crew.state = Crew.FOLLOWING;
						}
					}
				}
			}
		}
		if (Input.GetKeyUp(KeyCode.Mouse0))
		{//鼠标抬起
			Debug.Log("mouseUp");
			endPosition = Input.mousePosition;
			mouseDown = false;

			for (int i = 0; i < crews.Length; i++)
			{
				worldPosition = crews[i].transform.position;
				screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
				print(crews[i].characterName + " position:" + (Vector2)screenPosition);
				if (Judge(screenPosition))
				{
					crews[i].selected = true;
					print(crews[i].characterName + " selected!");
				}
			}
			
		}
		
	}

	bool Judge(Vector2 point)
	{
		float minx, maxx, miny, maxy;
		if(startPosition.x < endPosition.x)
		{
			minx = startPosition.x;
			maxx = endPosition.x;
		}
		else
		{
			minx = endPosition.x;
			maxx = startPosition.x;
		}
		if (startPosition.y < endPosition.y)
		{
			miny = startPosition.y;
			maxy = endPosition.y;
		}
		else
		{
			miny = endPosition.y;
			maxy = startPosition.y;
		}
		if(point.x >= minx && point.x <= maxx && point.y >= miny && point.y <= maxy)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	void ControlFieldOfView()
	{
		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			if(Camera.main.fieldOfView < 100)
			{
				Camera.main.fieldOfView += 2;
			}
			else
			{
				Camera.main.fieldOfView = 100;
			}		
		}
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			if (Camera.main.fieldOfView > 60)
			{
				Camera.main.fieldOfView -= 2;
			}
			else
			{
				Camera.main.fieldOfView = 60;
			}

		}
	}
	// Use this for initialization
	void Start()
	{
		gameManager = GameManager.Instance();
		mouseDown = false;
	}

	// Update is called once per frame
	void Update()
	{
		if (gameManager.opreateLock == false)
		{
			if (Input.GetMouseButtonDown(1))
			{
				ReadCharacterInfo();
			}
			SelectCharacter();
			ControlFieldOfView();
		}
	}
}
