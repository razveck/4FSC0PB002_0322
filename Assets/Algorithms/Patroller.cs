//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityIntro.Algorithms {

	public class Patroller : MonoBehaviour {

		public Transform[] Waypoints;

		private int _current;

		public Node StartNode;
		public Node CurrentNode;

		// Use this for initialization
		private void Start() {
			CurrentNode = StartNode;
		}

		// Update is called once per frame
		private void Update() {
			//if(Vector3.Distance(Waypoints[_current].position, transform.position) < 0.01f)
			//	_current++;

			//transform.Translate((Waypoints[_current].position - transform.position).normalized * Time.deltaTime);

			if(Vector3.Distance(CurrentNode.transform.position, transform.position) < 0.01f)
				CurrentNode = CurrentNode.Next;

			transform.Translate((CurrentNode.transform.position - transform.position).normalized * Time.deltaTime);
		}
	}
}
