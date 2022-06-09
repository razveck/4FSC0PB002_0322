using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shizounu.Library;
using Shizounu.Library.Editor;
using System.Net.Mail;
using UnityEngine.Events;

namespace UnityIntro.Erik.FPS
{
    public class HealthBase : MonoBehaviour
    {
        [Header("Alive")]
        [SerializeField, ReadOnly] private bool _Alive = true;
        public bool Alive{
            get {return _Alive;}
            set {
                _Alive = value;
                if(!_Alive)
                    onDeath?.Invoke();
            }
        }
        public UnityEvent onDeath;
        [Header("Health")]
        public float MaxHealth = 100;
        [SerializeField, ReadOnly] private float _Health;
        public float Health{
            get {return _Health;}
            set {
                _Health = value;
                if(_Health > MaxHealth)
                    _Health = MaxHealth;
                if(_Health <= 0)
                    Alive = false;
            }
        }

        private void Start() {
            Health = MaxHealth;
        }
    }
}
