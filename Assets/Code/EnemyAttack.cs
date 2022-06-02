using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityIntro{

public class EnemyAttack : AttackBase {

	public override void DoAttack() {
		var colliders = Physics.OverlapSphere(transform.position, _attackRange,_attackMask);
		if(colliders.Length > 0){
			Debug.Log($"Hit {colliders[0].gameObject.name}", colliders[0].gameObject);

			if(colliders[0].TryGetComponent(out HealthBase health)){
				health.TakeDamage(_damage);
			}
		}
	}

	public override bool ShouldAttack() {
		return Physics.CheckSphere(transform.position, _attackRange,_attackMask);
	}
}
}