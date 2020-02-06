using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilotController : MonoBehaviour {
	[SerializeField] Camera mainCamera;
	Animator animator;
	float speed;
	static float WALK_SPEED;
	static float RUN_SPEED;
	void Move()
	{
		Vector3 vector = new Vector3(0, 0, 0);
		bool isMove = false;
		if (Input.GetKey(KeyCode.W))
		{
			vector += Vector3.forward;
			isMove = true;
		}
		if (Input.GetKey(KeyCode.A))
		{
			vector += Vector3.left;
			isMove = true;
		}
		if (Input.GetKey(KeyCode.S))
		{
			vector += Vector3.back;
			isMove = true;
		}
		if (Input.GetKey(KeyCode.D))
		{
			vector += Vector3.right;
			isMove = true;
		}
		if (isMove) {
			CameraFollow();
			if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				animator.SetBool("Walk", false);
				animator.SetBool("Run", true);
				speed = RUN_SPEED;
			}
			else
			{
				animator.SetBool("Run", false);
				animator.SetBool("Walk", true);
				speed = WALK_SPEED;
			}
			transform.LookAt(transform.position + vector); 
			transform.Translate(vector * speed, Space.World);
		}
		else{
			animator.SetBool("Walk", false);
			animator.SetBool("Run", false);
			speed = WALK_SPEED;
		}
	}

	void CameraFollow()
	{
		mainCamera.transform.position = transform.position + new Vector3(0, 7.5f, -5);
	}
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		WALK_SPEED = Time.deltaTime;
		RUN_SPEED = 2 * Time.deltaTime;
		speed = WALK_SPEED;
		CameraFollow();
	}
	
	// Update is called once per frame
	void Update () {
		Move();
	}
}
