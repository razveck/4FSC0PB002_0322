using System.Collections.Generic;
using UnityEngine;

namespace Richie.TowerDefence
{
    public class TileMap : MonoBehaviour
    {
        [Header("Map Size")]
        public Vector2 grid = new(10f, 10f);

        [Header("References")]
        [SerializeField] private Transform _worldCenter;
        [SerializeField] private GameObject _worldTile;
        [SerializeField] private GameObject _pathTile;
        [SerializeField] private GameObject _entrance;
        [SerializeField] private GameObject _exit;

        private GameObject _tiles, _doors;
        private Vector3 _start, _end;
        public Dictionary<Vector2, TileInfo> WorldTiles;

        private PathCreation _pathFinder;
        private List<Vector2> _pathTiles;

        private void Awake()
        {
            WorldTiles = new Dictionary<Vector2, TileInfo>();
            _pathFinder = GetComponentInChildren<PathCreation>();

            _tiles = new GameObject("Tiles");
            _doors = new GameObject("Doors");
            _tiles.transform.parent = transform;
            _doors.transform.parent = transform;

            FindCenter();
        }

        private void Start()
        {
            _start = _pathFinder.Path.Head.Data;
            _end = _pathFinder.Path.Tail.Data;
            _pathTiles = _pathFinder.pathTiles;

            GenerateMap();
            GenerateDoors();
        }

        private void GenerateMap()
        {
            for (int x = 0; x < grid.x; x++)
            {
                for (int y = 0; y < grid.y; y++)
                {
                    Vector2 position = new(x, y);

                    if (_pathTiles.Contains(position))
                    {
                        GameObject path = Instantiate(_pathTile, new Vector3(x, 0f, y), Quaternion.identity, _tiles.transform);
                        TileInfo newPathTile = path.GetComponentInChildren<TileInfo>();
                        WorldTiles.Add(position, newPathTile);
                        newPathTile.SetCoords(position);
                        newPathTile.SetPathTile(true);
                        path.name = $"({x}, {y})";
                    }
                    else
                    {
                        GameObject tile = Instantiate(_worldTile, new Vector3(x, 0f, y), Quaternion.identity, _tiles.transform);
                        TileInfo newTile = tile.GetComponent<TileInfo>();
                        WorldTiles.Add(position, newTile);
                        newTile.SetCoords(position);
                        newTile.SetPathTile(false);
                        tile.name = $"({x}, {y})";
                    }
                }
            }
        }

        private void GenerateDoors()
        {
            Instantiate(_entrance, new Vector3(_start.x, 1f, _start.z), Quaternion.identity, _doors.transform);
            Instantiate(_exit, new Vector3(_end.x, 1f, _end.z), Quaternion.identity, _doors.transform);
        }

        private void FindCenter()
        {
            _worldCenter.position = new Vector3(grid.x / 2, grid.y, -1);
        }
    }
}
