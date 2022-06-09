using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shizounu.Library;
using Shizounu.Library.Editor;

namespace UnityIntro.Erik.FPS
{


    [SelectionBase]
    public class PlayerController : MonoBehaviour
    {
        [Header("Move Stats")]
        public float moveSpeed = 3.5f;
        public float jumpForce = 500f;
        public float maxRadians = .5f;

        [Header("Interact Stats")]
        public float interactRange = 5;

        [SerializeField, ReadOnly] private Interactible[] Interactibles;

        [Header("References")]
        public Rigidbody rb;
        public InputManager inputManager;
        public CameraController cameraController;

        private void Start()
        {
            inputManager = InputManager.Instance;
            rb = GetComponent<Rigidbody>();
        }
        private void FixedUpdate()
        {
            Move(inputManager.moveDir);
            Jump();
            interact();
        }

        void Move(Vector2 dir)
        {
            Vector3 forward = transform.forward;
            Vector3 right = transform.right;
            float speed = moveSpeed;


            Debug.DrawLine(transform.position, transform.position + forward, Color.blue, Time.fixedDeltaTime);
            Debug.DrawLine(transform.position, transform.position + right, Color.red, Time.fixedDeltaTime);

            rb.velocity = forward * speed * dir.y + right * speed * dir.x + new Vector3(0, rb.velocity.y, 0);

        }

        void Jump()
        {
            if (inputManager.jumpFlag && isGrounded())
            {
                inputManager.jumpFlag = false;
                rb.AddForce(Vector3.up * jumpForce);
            }
        }

        void interact()
        {
            Interactibles = FindObjectsOfType<Interactible>();
            if (inputManager.interactFlag)
            {
                float closestDist = float.MaxValue;
                Interactible inter = null;
                foreach (Interactible item in Interactibles)
                {
                    if (Vector3.Distance(transform.position, item.transform.position) < interactRange)
                    {
                        if (Vector3.Distance(transform.position, item.transform.position) < closestDist)
                        {
                            inter = item;
                            Debug.Log("Updated closest obj");
                        }
                    }
                }

                inputManager.interactFlag = false;
                if (inter != null)
                    inter.Interact();
                else
                    Debug.Log("No interactible close enough");
            }
        }

        bool isGrounded()
        {
            RaycastHit ray1;
            Physics.Raycast(transform.position + new Vector3(0, -0.005f, 0), Vector3.down, out ray1);
            return ray1.distance < .05f;
        }
    }
}