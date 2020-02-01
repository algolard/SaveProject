using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crew : Character {
	[SerializeField] Pilot pilot;
	NavMeshAgent agent;
	NavMeshObstacle obstacle;
	Animator animator;
	public int state;
	public const int STANDING = 0;
	public const int FOLLOWING = 1;
	public const int RELAXING = 2;
	public const int WORKING = 3;
	public const int FIGHTING = 4;
	public const float GIVE_UP_DISTANCE = 20;
	public const float RUN_DISTANCE = 9;
	public const float STOP_DISTANCE = 5;
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		obstacle = GetComponent<NavMeshObstacle>();
		state = FOLLOWING;
	}
	
	void Follow()
	{
		
		if (state == FOLLOWING)
		{
			if (Vector3.Distance(transform.position, pilot.transform.position) > STOP_DISTANCE)
			{
				if (Vector3.Distance(transform.position, pilot.transform.position) > GIVE_UP_DISTANCE)
				{
					state = STANDING;
					animator.SetBool("Walk", false);
					animator.SetBool("Run", false);
					agent.nextPosition = transform.position;
					agent.enabled = false;
					obstacle.enabled = true;
				}
				else
				{
					print(name + " move");
					if(Vector3.Distance(transform.position, pilot.transform.position) < RUN_DISTANCE)
					{
						animator.SetBool("Walk", true);
						animator.SetBool("Run", false);
					}
					else
					{
						animator.SetBool("Run", true);
						animator.SetBool("Walk", false);
					}
					obstacle.enabled = false;
					agent.nextPosition = transform.position;
					agent.enabled = true;
					agent.SetDestination(pilot.transform.position);
				}
			}
			else{
				print(name + "stop");
				transform.LookAt(pilot.transform.position);
				animator.SetBool("Walk", false);
				animator.SetBool("Run", false);
				agent.nextPosition = transform.position;
				agent.enabled = false;
				obstacle.enabled = true;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		Follow();
	}
}
