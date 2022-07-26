using UnityEngine;
using System.Collections.Generic;

namespace Richie.TowerDefence
{
    public class EnemySpawner : MonoBehaviour
    {
        private Vector3 _startPos, _endPos;
        private float _timer, _tempHealth;
        internal bool start = true, stop;
        private int _currentAmount;
        private bool _spawn;

        private TileMap _tileMap;
        private PathCreation _path;
        private GameObject _allEnemies;

        [Header("Spawner Setting")]
        [SerializeField] private int _amount;
        [SerializeField] private float _timeBetween;

        [Header("Per Round Modifiers")]
        [SerializeField] private float _amountModifer = 1;
        [SerializeField] private float _healthModifer = 1.5f;

        [Header("Enemies on Map")]
        public List<EnemyMovement> AllEnemies;

        [Header("References")]
        [SerializeField] private GameObject _enemies;
        [SerializeField] private EnemyHealth _enemyHealth;

        // events //
        public event UpdateLives OnEnemyExit;
        public delegate void UpdateLives();

        public event EnemyDeath OnEnemyDeath;
        public delegate void EnemyDeath();

        public event ChangeRound OnChangeRound;
        public delegate void ChangeRound();

        public event RoundOver OnRoundOver;
        public delegate void RoundOver();

        void Start()
        {
            _tileMap = GetComponent<TileMap>();
            _path = GetComponentInChildren<PathCreation>();

            _allEnemies = new GameObject("Enemies");
            _allEnemies.transform.parent = transform;

            _timer = _timeBetween;
            _startPos = _path.Path.Head.Data;
            _endPos = _path.Path.Tail.Data;
        }

        void Update()
        {
            SpawnEnemies();
            TrackEnemies();
            EndRound();
        }

        public void StartSpawn()
        {
            if (!_spawn)
            {
                _spawn = true;
                _tempHealth = _enemyHealth.health;
            }
            else
            {
                if (AllEnemies.Count <= 0)
                {
                    float amount = _amount * _amountModifer;

                    OnChangeRound?.Invoke();

                    _currentAmount = 0;
                    _amount = (int)amount;
                    _tempHealth *= _healthModifer;
                }
            }
        }

        private void TrackEnemies()
        {
            if (AllEnemies.Count == 0) return;

            foreach (var enemy in AllEnemies)
            {
                Vector3 pos = new (enemy.Position.x, enemy.Position.z, 0f);
            
                // add enemies to the world tiles //
                if (!_tileMap.WorldTiles[pos].Occupants.Contains(enemy))
                {
                    _tileMap.WorldTiles[pos].Occupants.Add(enemy);
                }

                // remove enemies from this list //
                if (enemy == null)
                {
                    OnEnemyDeath?.Invoke();
                    AllEnemies.Remove(enemy);
                    return;
                }

                // when enemy reaches the end //
                if (Vector3.Distance(enemy.transform.position, _endPos) <= 0.5f)
                {   
                    OnEnemyExit?.Invoke();
                    Destroy(enemy.gameObject);
                }
            }
        }

        private void SpawnEnemies()
        {
            if (!_spawn) return;

            _timer -= Time.deltaTime;

            if (_currentAmount < _amount && _timer <= 0f)
            {
                GameObject enemy = Instantiate(_enemies, _startPos, Quaternion.identity, _allEnemies.transform);
                EnemyMovement enemyPos = enemy.GetComponent<EnemyMovement>();

                enemy.GetComponent<EnemyHealth>().health = _tempHealth;
                AllEnemies.Add(enemyPos);

                _timer = _timeBetween;
                _currentAmount++;
            }

            if (AllEnemies.Count > 0)
            {
                start = false;
            }
        }

        private void EndRound()
        {
            if (stop) return;
            if (!start && AllEnemies.Count <= 0)
            {
                OnRoundOver?.Invoke();
                stop = true;
            }
        }
    }
}
