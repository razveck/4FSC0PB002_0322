//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityIntro {

	public class Health : HealthBase {
		public float maxHealth;
		public float maxDamage;

		protected override float Damage() {
			return maxDamage;
		}

		protected override float MaxHealth() {
			return maxHealth;
		}
	}
}
