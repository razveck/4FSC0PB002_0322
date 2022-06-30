using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Richie.HnS
{
    public class EnemyBase : MonoBehaviour
    {
        private Vector3 spawnPos;
        private Rigidbody2D _rb;

        [SerializeField] private float _range;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _stopDistance;
        [SerializeField] private float _turnModifier;
        [SerializeField] private Transform target;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            spawnPos = transform.position;
        }

        private void Update()
        {
            Move();
            SpawnReturn();
        }

        private void Move()
        {
            if (Vector2.Distance(transform.position, target.position) > _range ||
                Vector2.Distance(transform.position, target.position) < _stopDistance) return;

            Vector2 direction = target.position - transform.position;
            _rb.MovePosition(_rb.position + _moveSpeed * Time.fixedDeltaTime * direction.normalized);

            Rotate(target.position);
        }

        protected virtual void SpawnReturn()
        {
            if (Vector2.Distance(transform.position, target.position) < _range ||
                Vector2.Distance(transform.position, spawnPos) < _stopDistance) return;

            Vector2 direction = spawnPos - transform.position;
            _rb.MovePosition(_rb.position + _moveSpeed * Time.fixedDeltaTime * direction.normalized);

            Rotate(spawnPos);
        }

        private void Rotate(Vector3 targetPos)
        {
            Vector2 direction = targetPos - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _turnModifier);
        }
    }
}
