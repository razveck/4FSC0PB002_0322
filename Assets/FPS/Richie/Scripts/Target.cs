using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS_Richie
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private float health;

        public void TakeDamage(float amount)
        {
            health -= amount;

            if (health <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
