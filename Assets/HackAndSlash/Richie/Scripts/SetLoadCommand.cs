//Author: João Azuaga

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Richie;
using UnityEngine;

namespace UnityIntro.HackAndSlash.Richie.Scripts {
	public class SetLoadCommand : MonoBehaviour {

		public void SetLoad(bool value){
			SaveSystem.loadFromSave = value;
		}
	}
}
