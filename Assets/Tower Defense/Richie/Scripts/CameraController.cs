using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Richie.TowerDefence
{
    public class CameraController : MonoBehaviour
    {
        private PlayerInput _action;
        private Vector2 _direction, _scrollValue;

        [Header("Settings")]
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _scrollSpeed = 5f;
        [SerializeField] private float _zoomModifier = 3f;

        [Header("References")]
        [SerializeField] private Transform _camera;
        [SerializeField] private Transform _center;

        private void Awake()
        {
            _action = new PlayerInput();
            _action.defence.scroll.performed += ctx => Zoom();
            _action.defence.center.performed += ctx => Center();
        }

        private void Start()
        {
            Center();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            _direction = _action.defence.move.ReadValue<Vector2>();
            Vector3 input = (_direction.y * (_moveSpeed / 100) * transform.forward) + (_direction.x * (_moveSpeed / 100) * transform.right);
            transform.position += input;
        }

        public void Zoom()
        {
            _scrollValue = _action.defence.scroll.ReadValue<Vector2>();
            transform.position += _scrollValue.y / _zoomModifier * _scrollSpeed * Time.deltaTime * _camera.forward;
        }

        private void Center()
        {
            transform.position = _center.position;
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
