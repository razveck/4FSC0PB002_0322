using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBase : MonoBehaviour
{

	[SerializeField]
	protected float _attackRange = 5f;

	[SerializeField]
	protected LayerMask _attackMask = default;

	[SerializeField]
	protected float _damage = 1f;

    public abstract void DoAttack();
    public abstract bool ShouldAttack();

	private void Update() {
		if(ShouldAttack())
			DoAttack();
	}

}
