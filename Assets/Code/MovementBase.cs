//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityIntro {
	public abstract class MovementBase : MonoBehaviour {

		public Rigidbody Rigidbody;

		public float Speed = 1f;

		//Option 1: subclass calls this method in its Update
		//protected void Move(Vector3 movementInput) {
		//	Vector3 movementDirection = new Vector3(movementInput.x, 0f, movementInput.y);
		//	movementDirection = Normalize(movementDirection);

		//	Rigidbody.MovePosition(Rigidbody.position + Speed * Time.fixedDeltaTime * movementDirection);
		//}

		//Option 2: base class calls the subclass in its Update
		private void FixedUpdate() {
			Vector3 movementInput = GetDirection();
			Vector3 movementDirection = new Vector3(movementInput.x, 0f, movementInput.y);
			movementDirection = Normalize(movementDirection);
			
			Rigidbody.MovePosition(Rigidbody.position + Speed * Time.fixedDeltaTime * movementDirection);
		}

		protected abstract Vector3 GetDirection();

		private float GetMagnitude(Vector3 vector) {
			float h = Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);

			return h;
		}

		private float GetMagnitude(Vector2 vector) {
			float h = Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);

			return h;
		}

		private Vector3 Normalize(Vector3 vector) {
			float magnitude = GetMagnitude(vector);

			if(magnitude <= 0) {
				return vector;
			}

			//Vector3 result = new Vector3(vector.x / magnitude, vector.y / magnitude, vector.z / magnitude);
			//result.x = vector.x / magnitude;
			//result.y = vector.y / magnitude;
			Vector3 result = vector / magnitude;

			return result;
		}

	}
}
