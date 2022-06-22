using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityIntro.Erik.FPS;

namespace UnityIntro.Erik.Platformer
{
    public class CameraController_Player : MonoBehaviour
    {
        [Header("Stats")]
        public float MoveSpeed;
        public Vector2 verticalBounds;
        public Vector2 horizontalBounds;
        
        [Header("Body Rotation")]
        public Vector2 rotationThreshold;
        public float rotationSpeed = 5;

        [Header("References")]
        public Transform playerCamera;
        public InputManager input;
        public Vector2 cameraPosition;
        private void Start() {
            input = InputManager.Instance;
            playerCamera = Camera.main.transform;
        }
        private void LateUpdate() {
            Move(input.cameraDir);
            RotateBody();
        }

        public void Move(Vector2 dirDelta){
            //Camera Movespeed
            dirDelta *= MoveSpeed * Time.deltaTime;
            dirDelta.y *= -1;

            //Check movement left and right for camera to see if it remains inside its bounds 
            if (verticalBounds.x < cameraPosition.x + dirDelta.x &&
               verticalBounds.y > cameraPosition.x + dirDelta.x)
            {
                cameraPosition.x += dirDelta.x;
                playerCamera.forward = (Vector3)((CustomVector3)playerCamera.forward).RotateAroundY(dirDelta.x);
            }

            //Checks up and down bounds for the camera
            if (horizontalBounds.x < cameraPosition.y + dirDelta.y &&
               horizontalBounds.y > cameraPosition.y + dirDelta.y)
            {

                cameraPosition.y += dirDelta.y;
                playerCamera.forward = (Vector3)((CustomVector3)playerCamera.forward).RotateAroundX(dirDelta.y);
            }
            Mathf.Clamp(cameraPosition.x, verticalBounds.x, verticalBounds.y);
            Mathf.Clamp(cameraPosition.y, horizontalBounds.x, horizontalBounds.y);
        }

        public void RotateBody(){
            if (cameraPosition.x < rotationThreshold.x)
            {
                transform.forward = ((CustomVector3)transform.forward).RotateAroundY(rotationSpeed * Time.deltaTime); 
            }
            if (cameraPosition.x > rotationThreshold.y)
            {
                transform.forward = ((CustomVector3)transform.forward).RotateAroundY(-rotationSpeed * Time.deltaTime); 
            }
        }
    }
}
