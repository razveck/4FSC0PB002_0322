using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shizounu.Library.Editor;
using UnityEngine.Events;
namespace UnityIntro.Erik.HackAndSlash
{
    public class HackAndSlash_Health : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private bool _Alive = true;
        public bool Alive{
            get => _Alive;
            set{
                _Alive = value;
                if(!_Alive){
                    OnDeath?.Invoke();
                    StartCoroutine(DeathAnim());
                    
                }
            }
        }
        [SerializeField] private float _Health = 5;
        public float Health{
            get => _Health;
            set{
                _Health = value;
                if(value < 0)
                    OnHit?.Invoke();
                if(_Health <= 0)
                    Alive = false;
            }
        }
        [Space()]
        public UnityEvent OnDeath;
        public UnityEvent OnHit;

        [Space()]
        public float deathTime = 0.5f;
        public IEnumerator DeathAnim(){
            Renderer renderer = GetComponent<Renderer>();
            Color c = renderer.material.color;
            float a = 1;
            for (int i = 0; i < 60; i++){
                transform.localScale = Vector3.one * (float)(1 - (i / 60));
                c.a = a;
                renderer.material.color = c;
                a -= (float)(1 - (i / 60));
                yield return new WaitForSeconds(1/60 * deathTime);
            }
            this.enabled = false;
        }
    }
}
