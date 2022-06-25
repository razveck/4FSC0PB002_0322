using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAI : MonoBehaviour {
	
	
	[SerializeField]
	//float speed = 0.5f;


	Rigidbody Rigidbody;
	CharacterController controller;



	void Start() {
		controller = GetComponent<CharacterController>();
	}



	void FixedUpdate() {

		//hits
		
	}
}
