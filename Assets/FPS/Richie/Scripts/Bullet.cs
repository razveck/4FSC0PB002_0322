using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS_Richie
{
    public class Bullet : MonoBehaviour
    {
        public float Damage;

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.TryGetComponent<Target>(out Target target))
            {
                target.TakeDamage(Damage);
            }
        }
    }
}
