//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace UnityIntro.Erik.TowerDefense.Assets.Algorithms {
	public class DictionaryInEditor : MonoBehaviour {
		
		[Serializable]
		public class TileInfo{
			public Vector2 Position;
			public GameObject Obj;
		}

		public List<TileInfo> list;

		[Serializable]
		public class Vector2GameObjectDictionary : Dictionary<Vector2, GameObject>{

		}

		public Vector2GameObjectDictionary Dict = new Vector2GameObjectDictionary();

		private void OnValidate() {
			Dict.Clear();
			Dict.Add(Vector2.zero, null);
		}

	}
}
