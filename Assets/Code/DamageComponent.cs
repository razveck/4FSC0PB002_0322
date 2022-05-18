//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityIntro.Assets.Code {

	public class DamageComponent : MonoBehaviour {

		public List<damageResistance> Resists = new List<damageResistance>();

		public void dealDamage(damageStruct[] damage, ref float Health) {
			for(int i = 0; i < damage.Length; i++)
				dealDamage(damage[i], ref Health);
		}
		public void dealDamage(damageStruct damage, ref float Health) {
			float resistance = 0;

			for(int i = 0; i < Resists.Count; i++) {
				if(Resists[i].damageType == damage.damageType) {
					resistance += Resists[i].amount;
				}
			}

			Health -= damage.amount * (1 - resistance);
		}
	}

	public enum DamageType {
		Kinetic,
		Fire,
		Ice
	}

	public struct damageResistance {
		public DamageType damageType;
		public float amount;
	}

	[Serializable]
	public struct damageStruct {
		public damageStruct(float a, DamageType dt = DamageType.Kinetic) {
			damageType = dt;
			amount = a;
		}

		public DamageType damageType;
		public float amount;
	}
}
