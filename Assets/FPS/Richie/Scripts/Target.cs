using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS_Richie
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private float _health;
        [SerializeField] private float _respawnTime;
        [SerializeField] private GameObject _target;

        private float _counter, _currenthealth;
        private bool _respawn = false;

        private GameObject _temp;

        private void Awake()
        {
            _temp = Instantiate(_target, transform.position, Quaternion.identity);
            _counter = _respawnTime;
            _currenthealth = _health;
        }

        private void Update()
        {
            if (_respawn)
            {
                _counter -= Time.deltaTime;
                if (_counter <= 0f)
                {
                    _temp.SetActive(true);
                    _currenthealth = _health;
                    _respawn = false;
                }
            }
        }

        public void TakeDamage(float amount)
        {
            _currenthealth -= amount;

            if (_currenthealth <= 0f)
            {
                _temp.SetActive(false);
                _counter = _respawnTime;
                _respawn = true;
            }
        }
    }
}
