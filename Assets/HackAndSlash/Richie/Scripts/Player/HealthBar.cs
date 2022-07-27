using UnityEngine.UI;
using UnityEngine;

namespace Richie
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        public HealthBase Health;

        private void Start()
        {
            if(Health is not null)
                Initialize();
        }

        public void Initialize(){
            Health.OnHit += Health_OnHit;

            MaxHealth(Health.GetMaxHealth());
            SetHealth(Health.GetHealth());
        }

		private void OnDestroy() {
            Health.OnHit -= Health_OnHit;
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
