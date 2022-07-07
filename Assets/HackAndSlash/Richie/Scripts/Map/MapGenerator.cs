using System.Collections.Generic;
using UnityEngine;

namespace Richie
{
    public class MapGenerator : MonoBehaviour
    {
        public bool LoadLevel;

        [SerializeField]
        private SaveSystem _saveSystem;

        [Header("Map Settings")]
        [SerializeField] private Vector2Int _range;
        [SerializeField] private Vector2Int _minRoomSize;
        [SerializeField] private int _iterations;

        [Header("Tile References")]
        [SerializeField] private GameObject _wall;
        [SerializeField] private GameObject _floor;
        [SerializeField] private GameObject _corridor;

        [Header("Leaf References")]
        [SerializeField] private bool _showLeaves;
        [SerializeField] private GameObject[] _tiles;

        // tree info //
        private BinarySpacePartition _bsp;
        private Vector2Int _startPos = new(0, 0);

        // room info //
        public List<Room> Rooms = new();
        private readonly MapData data = new();
        private readonly List<Vector2Int> _allCenters = new();

        // tiles info //
        private readonly List<Vector2Int> _allFloors = new();
        private readonly List<Vector2Int> _allCorridors = new();
        private readonly Dictionary<Vector2Int, GameObject> _allWalls = new();

        // placeholders  //
        private readonly List<GameObject> _rooms = new();

        // number counters //
        private int _num = 0, _roomNum = 0, _leafNum = 0, _corridorNum = 1;

        private void Awake()
        {
            if (!SaveSystem.loadFromSave)
            {
                _bsp = new BinarySpacePartition();
                _bsp.AddRoot(_startPos, _range);
                _bsp.Split(_iterations, _minRoomSize);
                _bsp.GetLeaves();

                CreateRooms(_bsp.leafNodes);
            }
            else Load();

            SpawnRooms(Rooms);
            SpawnCorridors(Rooms);
            StoreRooms(Rooms);
        }

        private void Start()
        {
            _saveSystem.OnSave += SaveSystem_OnSave;
            _saveSystem.OnLoad += SaveSystem_OnLoad;
        }

        // initialilze //
        private void CreateRooms(List<Node> nodes)
        {
            if (nodes.Count <= 0) return;

            foreach (Node node in nodes)
            {
                CreateRooms(node.Origin, node.Range, _minRoomSize);
                if (_showLeaves) DrawLeaves(node.Origin, node.Range);
            }
        }

        private void CreateRooms(Vector2Int origin, Vector2Int range, Vector2Int minRoomSize)
        {
            Vector2Int start = new();
            Vector2Int newRange = new();
            Vector2Int maxOrigin = new((origin.x + range.x) - minRoomSize.x, (origin.y + range.y) - minRoomSize.y);

            // assign the x position of the start //
            if (range.x == minRoomSize.x)
            {
                start.x = origin.x;
            }
            else start.x = Random.Range(origin.x + 1, maxOrigin.x);

            // assign the y position of the start //
            if (range.y == minRoomSize.y)
            {
                start.y = origin.y;
            }
            else start.y = Random.Range(origin.y + 1, maxOrigin.y);

            // assign the x range based on x start position //
            if (start.x == maxOrigin.x)
            {
                newRange.x = minRoomSize.x;
            }
            else newRange.x = Random.Range(minRoomSize.x, (origin.x + range.x) - start.x);

            // assign the y range based on y start position //
            if (start.y == maxOrigin.y)
            {
                newRange.y = minRoomSize.y;
            }
            else newRange.y = Random.Range(minRoomSize.y, (origin.y + range.y) - start.y);

            // collect room information //
            Room newRoom = new()
            {
                Origin = start,
                Range = newRange,
                Center = new(start.x + (newRange.x / 2), start.y + (newRange.y / 2))
            };

            Rooms.Add(newRoom);
            _allCenters.Add(newRoom.Center);
        }

        private void CreateCorridors(Vector2Int position, Vector2Int destination)
        {
            GameObject corridor = new();
            corridor.transform.parent = _rooms[_corridorNum].transform;

            while (position.x != destination.x)
            {
                if (destination.x > position.x)
                {
                    position += Vector2Int.right;
                    DrawCorridors(position, corridor);
                }
                else
                {
                    position += Vector2Int.left;
                    DrawCorridors(position, corridor);
                }
            }

            while (position.y != destination.y)
            {
                if (destination.y > position.y)
                {
                    position += Vector2Int.up;
                    DrawCorridors(position, corridor);
                }
                else
                {
                    position += Vector2Int.down;
                    DrawCorridors(position, corridor);
                }
            }
            _corridorNum++;
        }


        // create //
        private void SpawnRooms(List<Room> Rooms)
        {
            foreach (Room room in Rooms)
            {
                DrawRooms(room.Origin, room.Range);
            }
        }

        private void SpawnCorridors(List<Room> room)
        {
            for (int i = 0; i < room.Count - 1; i++)
            {
                if (_allCenters.Count <= 0) return;

                _allCenters.Remove(room[i].Center);
                var temp = ClosestCenter(room[i].Center, _allCenters);
                CreateCorridors(room[i].Center, temp);
            }
        }


        // instantiate //
        private void DrawRooms(Vector2Int start, Vector2Int range)
        {
            GameObject room = new();
            GameObject walls = new();
            GameObject floors = new();

            room.transform.parent = transform;
            walls.transform.parent = room.transform;
            floors.transform.parent = room.transform;

            _rooms.Add(room);

            for (int x = start.x; x < (start.x + range.x); x++)
            {
                for (int y = start.y; y < (start.y + range.y); y++)
                {
                    walls.name = "Walls";
                    floors.name = "Floors";
                    room.name = $"Room {_roomNum}";
                    Vector2Int position = new(x, y);

                    if (position.x <= start.x || position.x >= (start.x + range.x) - 1 || position.y <= start.y || position.y >= (start.y + range.y) - 1)
                    {
                        GameObject newWall = Instantiate(_wall, (Vector2)position, Quaternion.identity, walls.transform);
                        newWall.name = $"({x}, {y})";
                        _allWalls.Add(position, newWall);
                    }
                    else
                    {
                        GameObject newFloor = Instantiate(_floor, (Vector2)position, Quaternion.identity, floors.transform);
                        newFloor.name = $"({x}, {y})";
                        _allFloors.Add(position);
                    }
                }
            }
            _roomNum++;
        }

        private void DrawLeaves(Vector2Int start, Vector2Int range)
        {
            GameObject leaf = new();
            leaf.transform.parent = transform;

            for (int x = start.x; x < (start.x + range.x); x++)
            {
                for (int y = start.y; y < (start.y + range.y); y++)
                {
                    Vector2 position = new(x, y);
                    if (_num == _tiles.Length - 1) _num = 1;
                    GameObject newTile = Instantiate(_tiles[_num], position, Quaternion.identity, leaf.transform);
                    leaf.name = $"Leaf {_leafNum}";
                    newTile.name = $"({x},{y})";
                }
            }
            _leafNum++;
            _num++;
        }

        private void DrawCorridors(Vector2Int position, GameObject corridor)
        {
            if (_allFloors.Contains(position)) return;

            GameObject tile = Instantiate(_corridor, (Vector2)position, Quaternion.identity, corridor.transform);
            AddWalls(position, corridor.transform);
            tile.name = $"({position.x}, {position.y})";
            _allCorridors.Add(position);
            corridor.name = "Corridor";

            if (_allWalls.ContainsKey(position))
                OpenDoor(position);
        }


        // utility //
        private void OpenDoor(Vector2Int position)
        {
            if (_allWalls.ContainsKey(position))
            {
                Destroy(_allWalls[position]);
            }
        }

        private void AddWalls(Vector2Int tile, Transform parent)
        {
            if (!_allWalls.ContainsKey(tile + Vector2Int.up) && !_allCorridors.Contains(tile + Vector2Int.up) && !_allFloors.Contains(tile + Vector2Int.up))
            {
                GameObject newWall = Instantiate(_wall, (Vector2)(tile + Vector2Int.up), Quaternion.identity, parent);
                newWall.name = $"({(tile + Vector2Int.up).x}, {(tile + Vector2Int.up).y})";
                _allWalls.Add(tile + Vector2Int.up, newWall);
            }

            if (!_allWalls.ContainsKey(tile + Vector2Int.down) && !_allCorridors.Contains(tile + Vector2Int.down) && !_allFloors.Contains(tile + Vector2Int.down))
            {
                GameObject newWall = Instantiate(_wall, (Vector2)(tile + Vector2Int.down), Quaternion.identity, parent);
                newWall.name = $"({(tile + Vector2Int.down).x}, {(tile + Vector2Int.down).y})";
                _allWalls.Add(tile + Vector2Int.down, newWall);
            }

            if (!_allWalls.ContainsKey(tile + Vector2Int.right) && !_allCorridors.Contains(tile + Vector2Int.right) && !_allFloors.Contains(tile + Vector2Int.right))
            {
                GameObject newWall = Instantiate(_wall, (Vector2)(tile + Vector2Int.right), Quaternion.identity, parent);
                newWall.name = $"({(tile + Vector2Int.right).x}, {(tile + Vector2Int.right).y})";
                _allWalls.Add(tile + Vector2Int.right, newWall);
            }

            if (!_allWalls.ContainsKey(tile + Vector2Int.left) && !_allCorridors.Contains(tile + Vector2Int.left) && !_allFloors.Contains(tile + Vector2Int.left))
            {
                GameObject newWall = Instantiate(_wall, (Vector2)(tile + Vector2Int.left), Quaternion.identity, parent);
                newWall.name = $"({(tile + Vector2Int.left).x}, {(tile + Vector2Int.left).y})";
                _allWalls.Add(tile + Vector2Int.left, newWall);
            }
        }

        private Vector2Int ClosestCenter(Vector2Int currentCenter, List<Vector2Int> allCenters)
        {
            Vector2Int closest = new();
            float distance = float.MaxValue;

            foreach (var center in _allCenters)
            {
                float currentDistance = Vector2.Distance(center, currentCenter);

                if (currentDistance < distance)
                {
                    distance = currentDistance;
                    closest = center;
                }
            }

            return closest;
        }


        // saving //
        private void SaveSystem_OnSave()
        {
            Save();
        }
        private void SaveSystem_OnLoad()
        {
            Load();
        }

        private void StoreRooms(List<Room> rooms)
        {
            foreach (var item in rooms) data.Rooms.Add(item);     
        }

        public void Save()
        {
            SaveSystem.SaveMap(data);
        }

        public void Load()
        {
            MapData data = SaveSystem.LoadMap() as MapData;
            Rooms = data.Rooms;
            foreach (Room room in Rooms) _allCenters.Add(room.Center);
        }
    }
}
