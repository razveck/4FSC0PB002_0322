using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityIntro {
	public class PlayerMovement : MovementBase {
		protected override Vector3 GetDirection() {
			Vector2 movementInput;
			movementInput.x = Input.GetAxis("Horizontal");
			movementInput.y = Input.GetAxis("Vertical");

			return movementInput;
		}
	}
}
