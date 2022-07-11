using System.Collections.Generic;
using UnityEngine;

namespace Richie.Simulation
{
    public class AnimalBase : MonoBehaviour
    {
        internal Map map;
        internal Spawner spawner;
        private List<Vector2> path;
        internal Vector2Int target, position;  
        protected bool _isTargeting, _isAdult;

        [SerializeField] internal AnimalType.Type type;

        [Header("Movement Settings")]
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _waitTime = 2f;
        [SerializeField] private float _refreshTime = 10f;
        private float _waitTimer = 0f, _refreshTimer = 0f;

        [Header("Growth Settings")]
        [SerializeField] private GameObject _adultForm;
        [SerializeField] private float _growTime = 20f;
        [SerializeField] private float _randomGrowth = 6f;
        private float _growTimer = 0f;

        [Header("Reproduction Settings")]
        [SerializeField] protected GameObject _child;
        [SerializeField] private float _reproduceTime = 20f;
        [SerializeField] private float _randomTime = 5f;
        [SerializeField] protected float _maxOffspring = 3f;
        public float _reproduceTimer = 0f;

        private void Start()
        {
            _growTimer = Random.Range(_growTime - _randomGrowth, _growTime + _randomGrowth);
            _reproduceTimer = Random.Range(_reproduceTime - _randomTime, _reproduceTime + _randomTime);
            if (!_isAdult) FindTarget();
        }

        private void Update()
        {
            Move();
            Grow();
            Reproduce();
        }

        protected virtual void Reproduce()
        {
            if (!_isAdult) return;

            _reproduceTimer -= Time.deltaTime;
            if (_reproduceTimer > 0) return;

            for (int i = 0; i < Random.Range(0, _maxOffspring + 1); i++)
            {
                if (_reproduceTimer <= 0)
                {
                    GameObject child = Instantiate(_child, (Vector2)position, Quaternion.identity, map.transform);
                    child.GetComponent<AnimalBase>().spawner = spawner;
                    child.GetComponent<AnimalBase>().map = map;
                    AnimalList(type).Add(child);
                }
            }

            _reproduceTimer = Random.Range(_reproduceTime - _randomTime, _reproduceTime + _randomTime);
        }

        private void Grow()
        {
            if (_isAdult) return;

            _growTimer -= Time.deltaTime;
            if (_growTimer <= 0)
            {
                Destroy(gameObject);
                GameObject adult = Instantiate(_adultForm, (Vector2)position, Quaternion.identity, map.transform);
                AnimalBase animal = adult.GetComponent<AnimalBase>();
                animal._isAdult = true;
                animal.spawner = spawner;
                animal.target = target;
                animal.map = map;

                AnimalList(type).Remove(gameObject);
                AnimalList(type).Add(adult);
            }
        }

        protected virtual void Move()
        {
            position = new(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
            if (!_isTargeting || _refreshTimer < 0f)
            {
                path = map.APath(position, target);
                _refreshTimer = _refreshTime;
                _isTargeting = true;

                if (path == null)
                {
                    Destroy(gameObject);
                    AnimalList(type).Remove(gameObject);
                    return;
                }
            }
            else _refreshTimer -= Time.deltaTime;

            if (Vector2.Distance(position, path[0]) < 0.01f && path.Count > 1) path.RemoveAt(0);

            if (position == target)
            {
                FindTarget();
                _isTargeting = false;
            }

            transform.Translate((new Vector3(path[0].x, path[0].y, 0f) - transform.position).normalized * _speed * Time.deltaTime);
        }

        protected virtual void FindTarget()
        {
            _waitTimer -= Time.deltaTime;
            if (_waitTimer > 0) return;

            target.x = Random.Range(0, map.range.x);
            target.y = Random.Range(0, map.range.y);

            _waitTimer = _waitTime;
            while (!map.PathFinder.AllNodes[target].IsValid)
            {
                target.x = Random.Range(0, map.range.x);
                target.y = Random.Range(0, map.range.y);
            }
        }

        private List<GameObject> AnimalList(AnimalType.Type type)
        {
            switch (type)
            {
                case AnimalType.Type.rabbit:
                    return spawner.rabbits;

                case AnimalType.Type.fox:
                    return spawner.foxes;

                default:
                    return null;
            }
        }
    }
}
