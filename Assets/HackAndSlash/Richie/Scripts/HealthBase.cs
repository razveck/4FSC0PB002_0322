using UnityEngine;

namespace Richie
{
    public class HealthBase : MonoBehaviour
    {
        [SerializeField] protected int _maxHealth = 100;
        [SerializeField] protected int _currentHealth;
        protected LootTable lootTable;

        public event HitTrigger OnHit;
        public delegate void HitTrigger(int value);

        public void SetHealth(int health, int maxHealth)
        {
            _currentHealth = health;
            _maxHealth = maxHealth;
        }

        public int GetHealth() => _currentHealth;
        public int GetMaxHealth() => _maxHealth;

        private void Start()
        {
            if (TryGetComponent<LootTable>(out _))
            {
                lootTable = GetComponent<LootTable>();
            }
            else lootTable = null;

            if (SaveSystem.loadFromSave) return;
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            OnHit?.Invoke(_currentHealth);

            if (_currentHealth <= 0f)
                OnDeath();
        }

        protected virtual void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}
