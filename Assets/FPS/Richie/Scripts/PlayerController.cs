using UnityEngine;
using Vectors_Richie;

namespace FPS_Richie
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody _rb;
        private PlayerInput _controller;
        private CapsuleCollider _collider;
        private Vector2 _input, _direction, _rotation;

        [Header("Camera")]
        [SerializeField] private Transform _camera;
        [SerializeField] private float _sensitivity = 8f;

        [Header("Movement")]
        [SerializeField] private float _moveSpeed = 8f;
        [SerializeField] private float _jumpHeight = 10f;
        [SerializeField] private float _inAirControl = 10f;
        private bool _canDrift;

        [Header("Ground Check")]
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private float _radius = 0.25f;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _collider = GetComponent<CapsuleCollider>();
            _controller = new PlayerInput();

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Start()
        {
            _controller.player.jump.performed += ctx => Jump();
        }

        private void Update()
        {
            _input = _controller.player.movement.ReadValue<Vector2>();
            _direction = _controller.player.camera.ReadValue<Vector2>();

            Camera();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Camera()
        {
            float mouseX = _direction.x * _sensitivity * Time.deltaTime;
            float mouseY = _direction.y * _sensitivity * Time.deltaTime;

            _rotation.y += mouseX;
            _rotation.x -= mouseY;
            _rotation.x = Mathf.Clamp(_rotation.x, -80f, 80f);

            _camera.localRotation = Quaternion.Euler(_rotation.x, _rotation.y, 0f);
            transform.localRotation = Quaternion.Euler(0f, _rotation.y, 0f);
        }

        private void Move()
        {
            if (IsGrounded())
            {
                Vector3 input = (_input.y * _moveSpeed * transform.forward) + (_input.x * _moveSpeed * transform.right);
                _rb.velocity = new Vector3(input.x, _rb.velocity.y, input.z);

                // can drift if jumped while standing still //
                if (_input == Vector2.zero)
                {
                    _canDrift = true;
                }
                else _canDrift = false;
            }
            else
            {
                if (!_canDrift) return;  

                Vector3 input = (_input.y * _inAirControl * transform.forward) + (_input.x * _inAirControl * transform.right);
                _rb.AddForce(input);
            }
        }

        private void Jump()
        {
            if (IsGrounded())
            {
                _rb.velocity = new Vector3(_rb.velocity.x, _jumpHeight, _rb.velocity.z);
            }
        }

        private bool IsGrounded()
        {
            Vector checkPos = new Vector(0f, _collider.height / 2f).Invert + _collider.center + transform.position;
            return Physics.CheckSphere(checkPos, _radius, _whatIsGround);
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
