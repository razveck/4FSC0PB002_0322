//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityIntro.HackAndSlash.Richie.Scripts {
	public class SceneLoader : MonoBehaviour {

		public void Load(int buildIndex){
			SceneManager.LoadScene(buildIndex);
		}

	}
}
