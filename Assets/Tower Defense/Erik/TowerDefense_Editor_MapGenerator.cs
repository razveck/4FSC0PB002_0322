using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

namespace UnityIntro.Erik.TowerDefense
{
    
    [CustomEditor(typeof(TowerDefense_MapGenerator))]
    public class TowerDefense_Editor_MapGenerator : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            
        }

        private void OnDrawGizmos() {
            
        }
    }
}
