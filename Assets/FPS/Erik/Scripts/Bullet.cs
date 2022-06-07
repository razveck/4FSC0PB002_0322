using System.Collections;
using System.Collections.Generic;
using Shizounu.Library;
using Unity.VisualScripting;
using UnityEngine;

namespace UnityIntro.Erik.FPS
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : MonoBehaviour
    {
        public float damage = 10;
        public float force = 250;
        public ObjectPool source;
        private void OnEnable() {
            GetComponent<Rigidbody>().AddForce(Vector3.forward * force);
        }
        private void OnDisable() {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        private void OnTriggerEnter(Collider other) {
            if(other.gameObject.TryGetComponent<HealthBase>(out HealthBase health)){
                health.Health -= damage;
            }
            Debug.Log("Returned", this.gameObject);
            source.returnElement(this.gameObject);
        }
    }
}
