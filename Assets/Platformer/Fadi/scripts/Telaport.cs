using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityIntro.Fadi.Platformer {
	public class Telaport : MonoBehaviour {
		private void OnTriggerEnter(Collider other) {
			if(other.gameObject.CompareTag("Player")) {
				Debug.Log(other);
				other.gameObject.transform.position = new Vector3(-11.84643f, 13.36982f, -23.09336f);

			}
		}

	}
}