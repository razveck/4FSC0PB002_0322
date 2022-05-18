//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace UnityIntro {
	public class EnemyMovement : MovementBase {
		protected override Vector3 GetDirection() {
			Vector2 movementInput;
			movementInput.x = 1f;
			movementInput.y = 0f;

			return movementInput;
		}
	}
}

public class EnemyAI : MonoBehaviour {
	public NavMeshAgent agent;
	public Transform Player;
	public LayerMask whatIsGround, whatIsPlayer;


	//patrolling
	public Vector3 walkpoint;
	bool walkPointSet;
	public float walkPointRange;


	//attacking
	public float timeBetweenAttacks;
	bool alreadyAttacked;


	//status
	public float sightRange, attackRange;
	public bool playerInSightRange, playerInAttackRange;




	// wake is called before the first frame update
	void Awake() {
		agent = GetComponent<NavMeshAgent>();


	}
	private void Update() {
		//check for sight and attack range
		playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
		playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
		if(!playerInSightRange && !playerInAttackRange) Patroling();
		if(playerInSightRange && !playerInAttackRange) ChasePlayer();
		if(playerInSightRange && playerInAttackRange) AttackPlayer();
	}


	private void Patroling() {
		if(!walkPointSet) SearchWalkPoint();


		if(walkPointSet)
			agent.SetDestination(walkpoint);


		Vector3 distanceToWalkPoint = transform.position - walkpoint;


		//walkpoint reached
		if(distanceToWalkPoint.magnitude < 1f)
			walkPointSet = false;
	}
	private void SearchWalkPoint() {
		float randomZ = Random.Range(-walkPointRange, walkPointRange);
		float randomX = Random.Range(-walkPointRange, walkPointRange);
		walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
		if(Physics.Raycast(walkpoint, -transform.up, 2f, whatIsGround))
			walkPointSet = true;


	}
	private void ChasePlayer() {
		agent.speed = 10;
		agent.SetDestination(Player.position);
	}
	private void AttackPlayer() {
		agent.SetDestination(transform.position);
		transform.LookAt(Player);
		if(!alreadyAttacked) {
			///attack dode here



			///


			alreadyAttacked = true;
			Invoke(nameof(ResetAttack), timeBetweenAttacks);
		}
	}
	private void ResetAttack() {
		alreadyAttacked = false;
	}

}
