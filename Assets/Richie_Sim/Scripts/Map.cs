using System.Collections.Generic;
using UnityEngine;

namespace Richie.Simulation
{
    public class Map : MonoBehaviour
    {
        [Header("Map Size")]
        [SerializeField] internal Vector2Int range;

        [Header("Test Settings")]
        [SerializeField] private float _duration = 8f;
        [SerializeField] private Vector2Int _testStart;
        [SerializeField] private Vector2Int _testEnd;

        [Header("References")]
        [SerializeField] internal GameObject[] grassTiles;
        [SerializeField] internal GameObject[] treeTiles;
        [SerializeField] internal GameObject[] waterTiles;

        public PathFinder PathFinder;
        internal GameObject _tileContainer;
        internal Dictionary<Vector2Int, GameObject> worldTiles;

        private void Awake()
        {
            worldTiles = new();
            PathFinder = new(range);

            _tileContainer = new();
            _tileContainer.name = "Tiles";
            _tileContainer.transform.parent = transform;

            GenerateMap();
        }

        private void GenerateMap()
        {
            int index;

            for (int x = 0; x < range.x; x++)
            {
                for (int y = 0; y < range.y; y++)
                {
                    float number = Random.Range(0f, 1f);

                    if (number < 0.9f) index = 0;
                    else index = Random.Range(1, grassTiles.Length);

                    Vector2Int position = new(x, y);
                    GameObject tile = Instantiate(grassTiles[index], (Vector2)position, Quaternion.identity, _tileContainer.transform);
                    worldTiles.Add(position, tile);
                    tile.name = $"({x}, {y})";
                }
            }
        }

        public List<Vector2> APath(Vector2Int start, Vector2Int end) => PathFinder.FindPath(start, end);

        [ContextMenu("Show Test Path")]
        public void APath()
        {
            List<Vector2> test = PathFinder.FindPath(_testStart, _testEnd);
            for (int i = 1; i < test.Count; i++)
            {
                Debug.DrawLine(test[i - 1], test[i], Color.green, _duration, true);
            }
        }   
    }
}
