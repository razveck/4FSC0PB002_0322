using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityIntro.Erik.TowerDefense
{
    public class TowerDefense_Tower : MonoBehaviour
    {
        public float damage = 1;
        public float Range = 5;

        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(transform.position, Range);
        }
    }
}
