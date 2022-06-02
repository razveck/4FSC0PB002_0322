using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class PlayerAttack : AttackBase {
	public override void DoAttack() {
		RaycastHit rh;
		if(Physics.Raycast(transform.position, transform.forward, out rh, _attackRange, ~(1 << gameObject.layer))) {
			Debug.Log($"Hit {rh.collider.gameObject.name}", rh.collider.gameObject);

			if(rh.collider.TryGetComponent(out HealthBase health)){
				health.TakeDamage(_damage);
			}
		}
	}

	public override bool ShouldAttack() {
		return Input.GetMouseButtonDown(0);
	}
}
