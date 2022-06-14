using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Shizounu.Library.Editor;
using Unity.VisualScripting;

namespace UnityIntro.Erik.FPS
{
    public class CameraController : MonoBehaviour
    {
        [Header("Camera Movement")]
        public float cameraMoveSpeed;
        public Vector2 yawRange;
        public Vector2 pitchRange;

        [Header("Head bobbing")]
        public bool doHeadBobbing;
        public float amplitude = .5f;
        public float frequency = 2.5f;

        [Header("Body Rotation")]
        public Vector2 rotationThreshold;
        public float rotationSpeed = 5;

        [Header("References")]
        public InputManager inputManager;
        public Transform pivot;
        public Transform root;
        [SerializeField, ReadOnly] private Vector2 cameraPos;
        [SerializeField, ReadOnly] private float time;
        public Vector3 camForward => pivot.forward;

        private void Start()
        {
            inputManager = InputManager.Instance;
        }

        private void LateUpdate()
        {
            Move(inputManager.cameraDir);
            HeadBobbing();
            BodyRotation();
        }

        void Move(Vector2 dirDelta)
        {
            //CameraPos is used to track rotation of the camera

            dirDelta *= cameraMoveSpeed * Time.deltaTime;
            dirDelta.y *= -1;
            //Check movement left and right for camera to see if it remains inside its bounds 
            if (yawRange.x < cameraPos.x + dirDelta.x &&
               yawRange.y > cameraPos.x + dirDelta.x)
            {
                cameraPos.x += dirDelta.x;
                //pivot.Rotate(0, dirDelta.x,0, Space.World);
                pivot.localEulerAngles += new Vector3(0, dirDelta.x, 0);
            }
            //Checks up and down bounds for the camera
            if (pitchRange.x < cameraPos.y + dirDelta.y &&
               pitchRange.y > cameraPos.y + dirDelta.y)
            {

                cameraPos.y += dirDelta.y;
                //pivot.Rotate(dirDelta.y, 0,0, Space.World);
                pivot.localEulerAngles += new Vector3(dirDelta.y, 0, 0);

            }
            Mathf.Clamp(cameraPos.x, yawRange.x, yawRange.y);
            Mathf.Clamp(cameraPos.y, pitchRange.x, pitchRange.y);
        }

        void BodyRotation()
        {
            if (cameraPos.x < rotationThreshold.x)
            {
                root.eulerAngles -= new Vector3(0, rotationSpeed * Time.deltaTime, 0);
            }
            if (cameraPos.x > rotationThreshold.y)
            {
                root.eulerAngles += new Vector3(0, rotationSpeed * Time.deltaTime, 0);
            }
        }
        void HeadBobbing()
        {
            if (!doHeadBobbing)
                return;
            if (inputManager.moveDir.magnitude > .5f)
            {
                Camera.main.transform.position = pivot.position + new Vector3(0, Mathf.Sin(time * frequency) * amplitude, 0);
                time += Time.deltaTime;
                if(time > Mathf.PI * 2)
                    time -= Mathf.PI * 2;
            }
        }
    
        private void OnDrawGizmos() {
           Gizmos.DrawRay(pivot.position, pivot.forward); 
        }
    }
}