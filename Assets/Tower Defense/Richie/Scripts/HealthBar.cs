using UnityEngine.UI;
using UnityEngine;

namespace Richie.TowerDefence
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private GameObject _canvas;

        private HealthBase _health;

        private void Start()
        {
            
            _health = GetComponentInParent<EnemyHealth>();
            _health.OnHealthBar += Health_OnHealthBar;

            MaxHealth(_health.health);
            _canvas.SetActive(false);
        }

        private void Health_OnHealthBar(float currentHealth) => SetHealth(currentHealth);

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
        private void OnDestroy() => _health.OnHealthBar -= Health_OnHealthBar;
    }
}
