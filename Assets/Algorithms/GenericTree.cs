//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityIntro.Erik.TowerDefense.Assets.Algorithms {
	public class GenericTree<T> {
		private T[] array;

		public Func<T, T, bool> SortingCondition;

		public void Sort() {
			for(int i = 0; i < array.Length - 1; i++) {
				bool isSorted = SortingCondition(array[i], array[i + 1]);
				//if(!isSorted)
				//Swap(array[i], array[i + 1]);
			}
		}
	}

	public class Program {
		void Main() {
			var tree = new GenericTree<int>();
			tree.SortingCondition = (a, b) => {
				//if a < b, return true
				//else return false
				return a > b;
			};

			var GOtree = new GenericTree<GameObject>();
			GOtree.SortingCondition = (a, b) => {
				return a.transform.childCount < b.transform.childCount;
			};

			var transformList = new List<Transform>();
			transformList.Sort((a, b) => {
				if(a.childCount < b.childCount)
					return -1;
				else
					return 1;

			});
		}
	}

	public class MyComparer : IComparer<int> {
		public int Compare(int a, int b) {
			if(a < b)
				return -1;
			else
				return 1;
		}
	}
}
