using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityIntro {
	public class PlayerMovement : MovementBase {
		protected override Vector3 GetDirection() {
			Vector3 movementInput;
			movementInput.x = Input.GetAxis("Horizontal");
			movementInput.y = 0f;
			movementInput.z = Input.GetAxis("Vertical");

			return movementInput;
		}
	}
}
