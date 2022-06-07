using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using Shizounu.Library;
namespace UnityIntro.Erik.FPS
{
    public class AttackRaycast : AttackBase
    {
        [SerializeField] private CameraController controller;
        [SerializeField] private LayerMask layerMask;

        private void FixedUpdate() {
            if(InputManager.Instance.attackFlag){
                InputManager.Instance.attackFlag = false;
                Attack();
            }
        }


        public override void Attack()
        {
            //Do the raycast
            RaycastHit rh = new();
            if(!Physics.Raycast(source.position, controller.camForward, out rh, Range, layerMask)){
                Debug.Log("Didnt hit anything");
                return; //handle hitting nothing
            }
            
            if(rh.transform.gameObject.TryGetComponent<HealthBase>(out HealthBase health)){
                health.Health -= Damage;
            }
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(source.position, controller.camForward);
        }
    }
}
