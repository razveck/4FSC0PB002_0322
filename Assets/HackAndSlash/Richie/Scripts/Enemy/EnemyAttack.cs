using UnityEngine;

namespace Richie
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private int _damage = 10;
        [SerializeField] private int _attackRate = 2;
        private float _nextAttackTime;

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.transform.TryGetComponent<PlayerHealth>(out _))
            {
                if (Time.time >= _nextAttackTime)
                {
                    other.transform.GetComponent<PlayerHealth>().TakeDamage(_damage);
                    _nextAttackTime = Time.time + 1f / _attackRate;
                }
            }
        }
    }
}
