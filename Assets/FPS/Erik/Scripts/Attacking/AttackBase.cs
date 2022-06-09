using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityIntro.Erik.FPS
{
    public abstract class AttackBase : MonoBehaviour
    {
        public float Damage;
        public float Range;
        public Transform source;
        public abstract void Attack();
    }
}
