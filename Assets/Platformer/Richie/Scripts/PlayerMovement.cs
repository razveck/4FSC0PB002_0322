using UnityEngine;
using Vectors_Richie;

namespace Richie.Platformer
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody _rb;
        private PlayerInput _controller;
        private CapsuleCollider _collider;

        [Header("Movement Settings")]
        [SerializeField] private float speed = 8f;
        [SerializeField] private float _jumpHeight = 10f;
        private Vector _input, _direction;

        [Header("Ground Check")]
        [SerializeField] private LayerMask _whatIsGround;
        [SerializeField] private float _groundCheckRadius = 0.3f;

        private void Awake()
        {
            _controller = new PlayerInput();
            _rb = GetComponent<Rigidbody>();
            _collider = GetComponent<CapsuleCollider>();
        }

        void Start()
        {
            _controller.player.jump.performed += ctx => Jump();
        }

        void Update()
        {
            Move();
        }

        private void Move()
        {
            _input = _controller.player.move.ReadValue<Vector2>();

            _direction = (_input.y * speed * transform.forward) + (_input.x * speed * transform.right);
            _rb.velocity = new Vector3(_direction.x, _rb.velocity.y, _direction.z);
        }

        private void Jump()
        {
            if (!IsGrounded()) return;

            _rb.velocity = new Vector(_rb.velocity.x, _jumpHeight, _rb.velocity.z);
        }

        public bool IsGrounded()
        {
            Vector3 checkPos = transform.position - new Vector3(0f, _collider.height / 2f) + _collider.center;
            return Physics.CheckSphere(checkPos, _groundCheckRadius, _whatIsGround);
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