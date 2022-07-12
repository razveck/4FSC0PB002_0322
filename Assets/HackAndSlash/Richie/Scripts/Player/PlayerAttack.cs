using UnityEngine;

namespace Richie
{
    public class PlayerAttack : MonoBehaviour
    {
        private PlayerInput _action;
        private bool _attack = false;
        private float _nextAttackTime = 0f;

        [SerializeField] private int _damage = 10;
        [SerializeField] private float _range = 0.25f;
        [SerializeField] private float _attackRate = 3.5f;

        [SerializeField] private Transform _attackPoint;
        [SerializeField] private LayerMask _enemyLayers;

        [SerializeField] private Animator _animator;

        private void Awake()
        {
            _action = new PlayerInput();
        }

        void Start()
        {
            _action.hackSlash.attack.performed += ctx => Attack();
        }

        private void Update()
        {
            if (Time.time >= _nextAttackTime)
            {
                _attack = true;
            }
            else _attack = false;
        }

        private void Attack()
        {
            if (!_attack) return;
            _animator.SetTrigger("attack");

            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _range, _enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHealth>().TakeDamage(_damage);
            }
            _nextAttackTime = Time.time + 1f / _attackRate;
        }

        private void OnDrawGizmosSelected()
        {
            if (_attackPoint == null) return;
            Gizmos.DrawWireSphere(_attackPoint.position, _range);   
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
