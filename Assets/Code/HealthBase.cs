//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityIntro {
	public abstract class HealthBase : MonoBehaviour {
		private float health;

		private void Start() {
			health = MaxHealth();
			Debug.Log($"{gameObject.name} has {health} health");

		}

		private void Update() {
			if(Input.GetKeyDown(KeyCode.Space)) {
				TakeDamage(10f);
				Debug.Log($"{gameObject.name} has {health} health remaining", gameObject);
			}
		}

		protected abstract float MaxHealth();
		protected abstract float Damage();

		private void TakeDamage(float damage) {
			if(damage < 0)
				Debug.LogError($"Damage is negative", gameObject);
			health -= damage;
			if(health <= 0f) {
				Destroy(gameObject);
				Debug.Log($"{gameObject.name} has died");
			}
		}

		private void OnCollisionEnter(Collision other) {
			if(other.transform.TryGetComponent<Health>(out Health target)) {
				TakeDamage(target.Damage());
				Debug.Log($"{gameObject.name} has {health} health remaining");
			}
		}

	}
}
