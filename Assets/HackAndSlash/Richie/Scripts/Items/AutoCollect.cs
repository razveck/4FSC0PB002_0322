using UnityEngine;

namespace Richie
{
    public class AutoCollect : MonoBehaviour
    {
        private float _timer;
        private Items.Item item;
        internal Transform target;
        internal PlayerBag playerBag;

        [SerializeField] private float _time = 1f;
        [SerializeField] private float _speed = 5f;

        private void Start()
        {
            _timer = _time;
            item = GetComponent<Items>().thisItem;
        }

        private void Update()
        {
            Collect();
        }

        private void Collect()
        {
            _timer -= Time.deltaTime;
            if (_timer > 0) return;

            Vector3 direction = target.position - transform.position;
            transform.position += _speed * Time.deltaTime * direction.normalized; 
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.transform.TryGetComponent<PlayerMove>(out _)) return;
            playerBag.Collected(item);
            Destroy(gameObject);
        }
    }
}
