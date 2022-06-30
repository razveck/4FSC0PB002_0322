using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityIntro.Erik.HackAndSlash
{
    /// <summary>
    /// https://www.youtube.com/playlist?list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v reference tutorial
    /// </summary>
    public class HeackAndSlash_DungeonGenerator : MonoBehaviour
    {
        

        [Header("Generation Parameters")]
        public Vector2Int StartPosition = Vector2Int.zero;
        public Vector2Int minRoomDimensions = new Vector2Int(4,4);
        public Vector2Int dungeonDimensions = new Vector2Int(20,20);
        [Range(0, 10)] public int offset = 1;
        public int EnemyCount = 10;

        [Header("References")]
        [SerializeField] private GameObject FloorTilePrefab;
        [SerializeField] private GameObject EnemyPrefab;

        [ContextMenu("Generate Map")]
        //Change it to X/Z 
        public void GenerateRooms(){
            var roomsList = BinarySpacePartitioning(
                new BoundsInt((Vector3Int)StartPosition, new Vector3Int(dungeonDimensions.x, 0, dungeonDimensions.y)),
                minRoomDimensions.x, 
                minRoomDimensions.y
            ); // <-- Not creating rooms/returning an empty list
            
            //create floor space for rooms
            HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
            foreach (var room in roomsList)
            {
                for (int col = offset; col < room.size.x - offset; col++)
                {
                    for (int row = offset; row < room.size.y - offset; row++)
                    {
                        Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                        floor.Add(position);
                    }
                }
            }
            //instantiate enemies in rooms
            for (int i = 0; i < EnemyCount; i++){
                int ind = Random.Range(0, floor.Count);
                Vector3 pos = new Vector3(floor.ToArray()[ind].x, 0, floor.ToArray()[ind].y);
                Instantiate(EnemyPrefab, pos, Quaternion.identity);
            }

            //create corridors between rooms
            List<Vector2Int> roomCenters = new List<Vector2Int>();
            foreach (var room in roomsList)
                roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
            
            HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
            floor.UnionWith(corridors);

            //instantiate floor tiles
            foreach (var tile in floor)
                Instantiate(FloorTilePrefab, new Vector3(tile.x, 0, tile.y), Quaternion.identity, this.transform);
            
            //create navmesh
            UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
        }

        #region BinarySpacePartioning
        public List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
        {
            Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
            List<BoundsInt> roomsList = new List<BoundsInt>();
            roomsQueue.Enqueue(spaceToSplit);
            while (roomsQueue.Count > 0)
            {
                var room = roomsQueue.Dequeue();
                if (room.size.y >= minHeight && room.size.x >= minWidth){
                    if (Random.value < 0.5f){
                        if (room.size.y >= minHeight * 2){
                            SplitHorizontally(minHeight, roomsQueue, room);
                        }
                        else if (room.size.x >= minWidth * 2){
                            SplitVertically(minWidth, roomsQueue, room);
                        }
                        else if (room.size.x >= minWidth && room.size.y >= minHeight){
                            roomsList.Add(room);
                        }
                    } else {
                        if (room.size.x >= minWidth * 2){
                            SplitVertically(minWidth, roomsQueue, room);
                        }else if (room.size.y >= minHeight * 2){
                            SplitHorizontally(minHeight, roomsQueue, room);
                        }else if (room.size.x >= minWidth && room.size.y >= minHeight){
                            roomsList.Add(room);
                        }
                    }
                }
            }
            return roomsList;
        }

        private void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
        {
            var xSplit = Random.Range(1, room.size.x);
            BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
            BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
                new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
            roomsQueue.Enqueue(room1);
            roomsQueue.Enqueue(room2);
        }

        private void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
        {
            var ySplit = Random.Range(1, room.size.y);
            BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
            BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
                new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
            roomsQueue.Enqueue(room1);
            roomsQueue.Enqueue(room2);
        }

        private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
        {
            HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
            var position = currentRoomCenter;
            corridor.Add(position);
            while (position.y != destination.y)
            {
                if (destination.y > position.y)
                {
                    position += Vector2Int.up;
                }
                else if (destination.y < position.y)
                {
                    position += Vector2Int.down;
                }
                corridor.Add(position);
            }
            while (position.x != destination.x)
            {
                if (destination.x > position.x)
                {
                    position += Vector2Int.right;
                }
                else if (destination.x < position.x)
                {
                    position += Vector2Int.left;
                }
                corridor.Add(position);
            }
            return corridor;
        }
        private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
        {
            HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
            var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
            roomCenters.Remove(currentRoomCenter);

            while (roomCenters.Count > 0)
            {
                Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
                roomCenters.Remove(closest);
                HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
                currentRoomCenter = closest;
                corridors.UnionWith(newCorridor);
            }
            return corridors;
        }
        private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
        {
            Vector2Int closest = Vector2Int.zero;
            float distance = float.MaxValue;
            foreach (var position in roomCenters)
            {
                float currentDistance = Vector2.Distance(position, currentRoomCenter);
                if (currentDistance < distance)
                {
                    distance = currentDistance;
                    closest = position;
                }
            }
            return closest;
        }
        #endregion
    }
}