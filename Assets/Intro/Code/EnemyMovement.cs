//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random; //type alias

namespace UnityIntro {
	public class EnemyMovement : MovementBase {
		[SerializeField]
		private float _baseSpeed;

		[SerializeField]
		private LayerMask _playerMask = default;

		private void Start() {
			_baseSpeed = Speed;
		}

		protected override Vector3 GetDirection() {
			if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitinfo, 1f, ~_playerMask)) {
				//check if raycast hits
				Debug.Log("hit");
				Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitinfo.distance, Color.red);

				//actually what happens
				int Direction = Random.Range(1, 4);
				switch(Direction) {
					case 1:
						transform.Rotate(new Vector3(0, -90, 0));
						break;
					case 2:
						transform.Rotate(new Vector3(0, 90, 0));
						break;
					case 3:
						transform.Rotate(new Vector3(0, 180, 0));
						break;
				}
			}
			//doesnt hit
			else {

				if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitinfo2, 20f, _playerMask)) {
					Speed = _baseSpeed * 2;
				} else {
					Speed = _baseSpeed;
				}
			}


			return transform.forward;
		}
	}
}