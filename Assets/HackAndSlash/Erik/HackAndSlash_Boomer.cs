using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace UnityIntro.Erik.HackAndSlash
{
    public class HackAndSlash_Boomer : MonoBehaviour
    {
        public float Damage = 1;
        public float Range = 2.5f;
        public LayerMask PlayerLayer;
        public GameObject explosionVFX;

        [ContextMenu("Boom")]
        public void Explode(){
            Collider[] hits = Physics.OverlapSphere(transform.position, Range, PlayerLayer);
            for (int i = 0; i < hits.Length; i++){
                if(hits[i].gameObject == this.gameObject)
                    continue;
                if(hits[i].gameObject.TryGetComponent<HackAndSlash_Health>(out HackAndSlash_Health health)){
                    health.Health -= Damage;
                }
            }
            GameObject go = Instantiate(explosionVFX, transform.position, transform.rotation);
            go.transform.localScale *= Range;
            Destroy(go, 3.5f);
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, Range);
        }
    }
}
