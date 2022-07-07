using UnityEngine;

namespace Richie
{
    public class EnemyMove : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Vector3 _spawnPos;
        public Transform Target;

        [SerializeField] private float _range;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _stopDistance;
        [SerializeField] private float _turnModifier;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _spawnPos = transform.position;
        }

        private void Update()
        {
            Move();
            SpawnReturn();
        }

        private void Move()
        {
            if (Vector2.Distance(transform.position, Target.position) > _range ||
                Vector2.Distance(transform.position, Target.position) < _stopDistance) return;

            Vector2 direction = Target.position - transform.position;
            _rb.MovePosition(_rb.position + _moveSpeed * Time.fixedDeltaTime * direction.normalized);

            Rotate(Target.position);
        }

        protected virtual void SpawnReturn()
        {
            if (Vector2.Distance(transform.position, Target.position) < _range ||
                Vector2.Distance(transform.position, _spawnPos) < _stopDistance) return;

            Vector2 direction = _spawnPos - transform.position;
            _rb.MovePosition(_rb.position + _moveSpeed * Time.fixedDeltaTime * direction.normalized);

            Rotate(_spawnPos);
        }

        private void Rotate(Vector3 targetPos)
        {
            Vector2 direction = targetPos - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Quaternion rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _turnModifier);
        }

        public Vector3 GetOrigin() => _spawnPos;

        public void SetOrigin(Vector3 pos) => _spawnPos = pos; 

    }
}
