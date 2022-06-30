using System.Collections;
using System.Collections.Generic;
using Shizounu.Library.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace UnityIntro.Erik.HackAndSlash
{
    public class HackAndSlash_PlayerController : MonoBehaviour
    {

        [Header("Stats")]
        public float dashRange = 3;
        [Header("References")]
        public NavMeshAgent navAgent;
        public HackAndSlash_Input input;

        [Header("Flags")]
        [SerializeField, ReadOnly] private Vector2 movementDirection;
        [SerializeField, ReadOnly] private bool dashFlag;
        [SerializeField, ReadOnly] private Vector2 mousePos;
        private void Awake() {
            input = new();

            input.Player.Move.performed += ctx => movementDirection = ctx.ReadValue<Vector2>();
            input.Player.Move.canceled += _ => movementDirection = Vector2.zero;

            input.Player.Dash.performed += ctx => dashFlag = true;

            input.Player.MousePos.performed += ctx => mousePos = ctx.ReadValue<Vector2>();
            navAgent.updateRotation = false;
        }

        private void Update() {
            Move(movementDirection);
            Dash(movementDirection);
            Rotate(mousePos);
        }

        void Move(Vector2 dir){
            Vector3 moveDir = new Vector3(dir.x, 0, dir.y);
            navAgent.Move(moveDir * navAgent.speed * Time.deltaTime);
        }
        void Dash(Vector2 dir){
            if(!dashFlag)
                return;
            Vector3 moveDir = new Vector3(dir.x, 0, dir.y);
            Vector3 movePos = transform.position + moveDir * dashRange;
            navAgent.Warp(movePos);
            dashFlag = false;
        }
        void Rotate(Vector2 mousePos){
            //Vector3 position = new Vector3(mousePos.x, mousePos.y, 0);
            //Ray hitRay = Camera.main.ScreenPointToRay(position, Camera.MonoOrStereoscopicEye.Mono);
            
            //Physics.Raycast(hitRay, out RaycastHit rh);
            //Vector3 dir = (rh.point - transform.position).normalized;

            //transform.forward = transform.position + dir;

            
            var screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = ((Vector3)mousePos - screenPoint).normalized;

            var worldDir = new Vector3(dir.x, 0, dir.y);

            transform.forward = worldDir;
            
        }

        private void OnEnable() => input.Enable();
        
        private void OnDisable() => input.Disable();
        
    }
}
