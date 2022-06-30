using UnityEngine;

namespace Richie.HnS
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private PlayerInput _action;
        private Vector2 _input, _mousePos;
        private Camera _camera;

        [SerializeField] private float _speed = 6f;

        private void Awake()
        {
            _camera = Camera.main;
            _action = new PlayerInput();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _input = _action.player.movement.ReadValue<Vector2>();
            _mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        }

        private void FixedUpdate()
        {
            Move();
            Rotate();
        }

        private void Move()
        {
            Vector2 direction = new (_input.x, _input.y);
            _rb.MovePosition(_rb.position + _speed * Time.fixedDeltaTime * direction);

        }

        private void Rotate()
        {
            Vector2 direction = _mousePos - _rb.position;
            _rb.rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        }

        private void OnEnable()
        {
            _action.Enable();
        }

        private void OnDisable()
        {
            _action.Disable();
        }
    }
}
