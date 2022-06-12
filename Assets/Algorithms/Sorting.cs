//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityIntro.Assets.Algorithms {


	public class Sorting : MonoBehaviour {

		public int[] Array;

		[ContextMenu("BubbleSort")]
		public void BubbleSort() {
			int temp;
			bool isSorted = false;
			while(!isSorted) {
				isSorted = true;
				for(int i = Array.Length - 1; i > 0; i--) {

					if(Array[i] < Array[i - 1]) {
						temp = Array[i];
						Array[i] = Array[i - 1];
						Array[i - 1] = temp;
						isSorted = false;
					}
				}
			}

		}

		[ContextMenu("Selection Sort")]
		public void SelectionSort() {
			void swap(ref int el1, ref int el2) {
				int t = el1;
				el1 = el2;
				el2 = t;
			}

			bool isSorted(int[] inp) {
				bool res = true;
				for(int i = 1; i < inp.Length; i++) {
					if(inp[i] < inp[i - 1])
						res = false;
				}
				return res;
			}

			int counter = 0;
			while(!isSorted(Array)) {
				//find highest
				int highestIndex = int.MinValue;
				for(int i = 0; i < Array.Length - counter; i++)
					if(highestIndex < 0 || Array[i] > Array[highestIndex])
						highestIndex = i;
				counter++;
				swap(ref Array[highestIndex], ref Array[Array.Length - counter]);
			}
			

		}
	}
}
