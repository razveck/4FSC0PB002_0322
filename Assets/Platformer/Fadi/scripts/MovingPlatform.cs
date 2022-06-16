using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityIntro.Fadi.Platformer {
	public class MovingPlatform : MonoBehaviour {
		public Vector3 moveDirectionAndSpeed;
		public float moveTime = 2;
		public float timer = 0;
		int moveDirection = 1;
		Vector3 oldPosition;

		private void Start() {
			oldPosition = transform.position;
		}

		// Update is called once per frame
		void Update() {
			timer += Time.deltaTime;
			if(timer >= moveTime) {
				timer = 0;
				moveDirection *= -1;
			}
			MovePlatformInDirection(moveDirection);
			Vector3 movementAmount = transform.position - oldPosition;

			oldPosition = transform.position;
		}

		void MovePlatformInDirection(int direction) {
			transform.position += moveDirectionAndSpeed * Time.deltaTime * direction;
		}

		private void OnTriggerEnter(Collider other) {
			if(other.gameObject.CompareTag("Player")) {
				other.gameObject.transform.SetParent(transform);
			}
		}
		private void OnTriggerExit(Collider other) {
			if(other.gameObject.CompareTag("Player")) {
				other.gameObject.transform.SetParent(null);
			}
		}
	}
}