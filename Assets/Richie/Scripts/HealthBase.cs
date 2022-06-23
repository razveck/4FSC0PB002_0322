using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Richie.TowerDefence
{
    public abstract class HealthBase : MonoBehaviour
    {
        public float health = 25f;

        public event ChangeHealthBar OnHealthBar;
        public delegate void ChangeHealthBar(float change); 

        private void Start()
        {
            health = MaxHealth();
        }

        protected abstract float MaxHealth();

        public void TakeDamage(float damage)
        {
            health -= damage;
            OnHealthBar?.Invoke(health);

            if (health <= 0f)
                OnDeath();
        }

        protected virtual void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}
