using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

namespace UnityIntro.Erik.AnimalSandbox
{
    [DefaultExecutionOrder(-50)]
    public class AnimalSandbox_GameManager : MonoBehaviour
    {
        public static AnimalSandbox_GameManager Instance { get; protected set; }
        private void Awake() {
            if(Instance != null){
                Destroy(this.gameObject);
            }
            Instance = this;
        }

        [ContextMenu("Tick")]
        public void DoTick(){

        }

        /// <summary>
        /// Time for Animations to execute
        /// </summary>
        public float AnimationTime = 1;

        [Header("Animal Prefabs")]
        public GameObject Rabbit;

        public void summonRabit(AnimalSandbox_TileManager tm){
            AnimalSandbox_AnimalManager t = Instantiate(Rabbit, transform.position, Quaternion.identity).GetComponent<AnimalSandbox_AnimalManager>();
            t.RegisterWithTile(tm);
        }
    }
}
