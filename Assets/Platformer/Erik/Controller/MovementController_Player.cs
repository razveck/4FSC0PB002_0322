using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.Profiling.Editor;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityIntro.Erik.FPS;

namespace UnityIntro.Erik.Platformer
{
    public class MovementController_Player : MonoBehaviour
    {
        [Header("Move Stats")]
        public float MoveSpeed;
        public float JumpForce;
        [Header("References")]
        public Rigidbody rb;
        public InputManager input;
        private void Start() {
            input = InputManager.Instance;
        }
        private void FixedUpdate() {
            Move(input.moveDir);
            Jump(input.jumpFlag);
                input.jumpFlag = false;
        }

        public void Move(Vector2 dir){
            CustomVector3 forward = (CustomVector3)transform.forward;
            CustomVector3 right = (CustomVector3)transform.right;

            CustomVector3 movement = (forward * dir.y + right * dir.x) * MoveSpeed;

            rb.velocity = (Vector3)(movement + new CustomVector3(0, rb.velocity.y, 0));
        }
        public void Jump(bool shouldJump){
            if(!shouldJump) return;
            rb.AddForce((Vector3)CustomVector3.Up * JumpForce);
        }
    }

}
