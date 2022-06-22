using System.Collections.Generic;
using UnityEngine;

namespace Richie.TowerDefence
{
    public class TowerBase : MonoBehaviour
    {
        private TileMap _tileMap;

        [Header("Tower Stats")]
        public string Name = "Gunner";
        public int Range = 1;
        public int Damage = 5;
        public float FireRate = 1;
        public int Cost = 20;

        [Header("Tower Info")]
        public Vector2 Coordinates;
        public List<Vector2> _neighbors;
        public List<TileInfo> _tilesInRange;

        [Header("Target Info")]
        private TileInfo info;
        private GameObject target;
        private bool findTarget = true;
        private float nextTimeToFire = 0f;

        private void Awake()
        {
            _tileMap = FindObjectOfType<TileMap>();
        }

        private void Start()
        {
            FindNeighbors();
            AddNeighbors();
        }

        private void Update()
        {
            FindTarget();
            Attack();
            RemoveTarget();
        }

        private void Attack()
        {
            if (findTarget) return;

            if (target == null)
            {
                target = null;
                findTarget = true;
                return;
            }

            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1 / (FireRate / 10);
                target.GetComponent<EnemyHealth>().TakeDamage(Damage);
            }
        }

        private void FindTarget()
        {
            if (!findTarget || target != null) return;

            foreach (var item in _tilesInRange)
            {
                if (item.Occupants.Count > 0 && item.Occupants[0] != null)
                {
                    info = item;
                    target = item.Occupants[0].transform.gameObject;       
                    findTarget = false;
                }
            }
        }

        private void RemoveTarget()
        {
            if (findTarget) return;

            if (info.Occupants.Count <= 0)
            {
                target = null;
                findTarget = true;
            }
        }

        private void FindNeighbors()
        {
            Vector2 startPos = new (transform.position.x - Range, transform.position.z - Range);
            Vector2 endPos = new(transform.position.x + Range, transform.position.z + Range);

            for (int x = (int)startPos.x; x <= endPos.x; x++)
            {
                for (int y = (int)startPos.y; y <= endPos.y; y++)
                {
                    _neighbors.Add(new Vector2(x, y));
                }
            }
        }

        private void AddNeighbors()
        {
            for (int i = 0; i < _neighbors.Count; i++)
            {
                if (_tileMap.WorldTiles.ContainsKey(_neighbors[i]) && _tileMap.WorldTiles[_neighbors[i]].IsPathTile == true)
                {
                    _tilesInRange.Add(_tileMap.WorldTiles[_neighbors[i]]);
                }
            }
        }
    }
}
