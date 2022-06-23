using UnityEngine.UI;
using UnityEngine;

namespace Richie.TowerDefence
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private GameObject _canvas;

        private void Start()
        {
            HealthBase health = GetComponentInParent<EnemyHealth>();
            health.OnHealthBar += Health_OnHealthBar;

            MaxHealth(health.health);
            _canvas.SetActive(false);
        }

        private void Health_OnHealthBar(float currentHealth)
        {
            SetHealth(currentHealth);
        }

        public void MaxHealth(float health)
        {
            slider.maxValue = health;
            slider.value = health;
        }

        public void SetHealth(float health)
        {
            slider.value = health;
            if (slider.value < slider.maxValue)
                _canvas.SetActive(true);
        }
    }
}
