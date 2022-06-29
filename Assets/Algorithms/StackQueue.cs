//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityIntro.Erik.TowerDefense.Assets.Algorithms {
	public class StackQueue : MonoBehaviour {

		public Stack<int> MyStack = new();
		public Queue<int> MyQueue = new();

		// Use this for initialization
		private void Start() {
			MyStack.Push(1);
			MyStack.Push(2);
			print(MyStack.Pop());

			MyQueue.Enqueue(1);
			MyQueue.Enqueue(2);
			print(MyQueue.Dequeue());

		}

		// Update is called once per frame
		private void Update() {

		}
	}
}
