using UnityEngine;
using Vectors_Richie;

namespace Richie.Platformer
{
    public class CameraRotation : MonoBehaviour
    {
        private PlayerInput _controller;

        [Header("Camera Settings")]
        [SerializeField] private Transform _camera;
        [SerializeField] private float _sensitivity = 7f;
        private Vector _rotation = new();
        private Vector _mouse = new();

        private void Awake()
        {
            _controller = new PlayerInput();
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            _mouse = _controller.player.camera.ReadValue<Vector2>();

            Rotation();
        }

        private void Rotation()
        {
            _rotation.x += _mouse.x * _sensitivity * Time.deltaTime;
            _rotation.y -= _mouse.y * _sensitivity * Time.deltaTime;
            _rotation.y = Mathf.Clamp(_rotation.y, 2.5f, 3.25f);

            Vector xPos = new(_camera.localPosition.x, 0f, _camera.localPosition.z);

            Vector xRot = Vector.RotateAroundY(xPos, _rotation.x);
            Vector yRot = Vector.RotateAroundX(_camera.localPosition, _rotation.y);

            transform.forward = xRot;
            _camera.forward = new Vector(xRot.x, yRot.y, xRot.z);
        }

        private void OnEnable()
        {
            _controller.Enable();
        }

        private void OnDisable()
        {
            _controller.Disable();
        }
    }
}