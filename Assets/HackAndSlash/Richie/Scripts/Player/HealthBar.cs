using UnityEngine.UI;
using UnityEngine;

namespace Richie
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Spawner _spawner;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private void Start()
        {
            PlayerHealth health = _spawner.currentPlayer.GetComponentInChildren<PlayerHealth>();
            health.OnHit += Health_OnHit;

            MaxHealth(health.GetMaxHealth());
            SetHealth(health.GetHealth());
        }

        private void Health_OnHit(int health)
        {
            SetHealth(health);
        }

        public void MaxHealth(int health)
        {
            _slider.maxValue = health;
            _slider.value = health;
        }

        public void SetHealth(int health)
        {
            _slider.value = health;
        }
    }
}
