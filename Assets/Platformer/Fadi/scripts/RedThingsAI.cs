using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityIntro.Fadi.Platformer {
	public class RedThingsAI : MonoBehaviour {
		public float speed;
		CharacterController controller;
		private int direction = 1;
		GameObject gameObject;
		private void Start() {
			controller = GetComponent<CharacterController>();
		}

		void FixedUpdate() {
			controller.Move(transform.forward * direction * speed);
			if(Physics.Raycast(transform.position, transform.forward * direction, out RaycastHit hitinfo, 1f)) {
				Debug.DrawRay(transform.position, transform.forward * hitinfo.distance, Color.red);
				if(direction == 1)
					direction = -1;
				else
					direction = 1;
			} else {
				Debug.DrawRay(transform.position, transform.forward * hitinfo.distance, Color.white);

			}
		}
		private void OnCollisionEnter(Collision collision) {
			if(collision.gameObject.CompareTag("Player")) {
				collision.transform.position = new Vector3(28.53041f, 11.64815f, -41.2795f);
			}
		}

	}
}