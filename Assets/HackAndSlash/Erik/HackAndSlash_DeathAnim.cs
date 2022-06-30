using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace UnityIntro.Erik.HackAndSlash
{
    public class HackAndSlash_DeathAnim : MonoBehaviour
    {
        public float deathTime = .5f;
        public float scale = 60;
        private void Update() {
            transform.localScale *= (scale / 60);
            scale -= Time.deltaTime * (1/deathTime);
        }
        
    }
}
