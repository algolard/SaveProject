using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Crew : Character {
	[SerializeField] Pilot pilot;
	NavMeshAgent agent;
	NavMeshObstacle obstacle;
	Animator animator;
	int state;
	const int STANDING = 0;
	const int FOLLOWING = 1;
	const int RELAXING = 2;
	const int WORKING = 3;
	const int FIGHTING = 4;
	const float GIVE_UP_DISTANCE = 20;
	const float RUN_DISTANCE = 7;
	const float STOP_DISTANCE = 3;
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
