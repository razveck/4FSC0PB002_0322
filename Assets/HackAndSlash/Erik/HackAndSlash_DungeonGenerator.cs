using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

namespace UnityIntro.Erik.HackAndSlash
{
    /// <summary>
    /// https://www.youtube.com/playlist?list=PLcRSafycjWFenI87z7uZHFv6cUG2Tzu9v reference tutorial
    /// </summary>
    public class HackAndSlash_DungeonGenerator : MonoBehaviour
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

        [SerializeField] private List<Transform> createdObjects;

        [ContextMenu("Generate Map")]
        //Change it to X/Z 
        public void GenerateRooms(){
            DeleteMap();
            var roomsList = BinarySpacePartitioning(
                new BoundsInt(new Vector3Int(StartPosition.x, 0, StartPosition.y), new Vector3Int(dungeonDimensions.x, 0, dungeonDimensions.y)),
                minRoomDimensions.x, 
                minRoomDimensions.y
            ); 
            
            //create floor space for rooms
            HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
            foreach (var room in roomsList)
            {
                for (int col = offset; col < room.size.x - offset; col++) {
                    for (int row = offset; row < room.size.z - offset; row++){
                        Vector2Int position = new Vector2Int(room.min.x, room.min.z) + new Vector2Int(col, row);
                        floor.Add(position);
                        
                        
                    }
                }
            }
            //instantiate enemies in rooms
            for (int i = 0; i < EnemyCount; i++){
                int ind = Random.Range(0, floor.Count);
                Vector3 pos = new Vector3(floor.ToArray()[ind].x, 0, floor.ToArray()[ind].y);
                createdObjects.Add(Instantiate(EnemyPrefab, pos, Quaternion.identity).transform);
            }

            //create corridors between rooms
            List<Vector2Int> roomCenters = new List<Vector2Int>();
            foreach (var room in roomsList)
                roomCenters.Add(new Vector2Int(Mathf.RoundToInt(room.center.x), Mathf.RoundToInt(room.center.z)));
            
            HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
            floor.UnionWith(corridors);

            //instantiate floor tiles
            foreach (var tile in floor)
                createdObjects.Add(Instantiate(FloorTilePrefab, new Vector3(tile.x, 0, tile.y), Quaternion.identity, this.transform).transform);
            
            //create navmesh
            UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
        }

        [ContextMenu("Delete Map")]
        void DeleteMap(){
            foreach (var item in createdObjects){
                DestroyImmediate(item.gameObject);
            }
            createdObjects = new();
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
                if (room.size.z >= minHeight && room.size.x >= minWidth){
                    if (Random.value < 0.5f){
                        if (room.size.z >= minHeight * 2){
                            SplitHorizontally(minHeight, roomsQueue, room);
                        }
                        else if (room.size.x >= minWidth * 2){
                            SplitVertically(minWidth, roomsQueue, room);
                        }
                        else if (room.size.x >= minWidth && room.size.z >= minHeight){
                            roomsList.Add(room);
                        }
                    } else {
                        if (room.size.x >= minWidth * 2){
                            SplitVertically(minWidth, roomsQueue, room);
                        }else if (room.size.z >= minHeight * 2){
                            SplitHorizontally(minHeight, roomsQueue, room);
                        }else if (room.size.x >= minWidth && room.size.z >= minHeight){
                            roomsList.Add(room);
                        }
                    }
                }
            }
            return roomsList;
        }

        private void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room) {
            var xSplit = Random.Range(1, room.size.x);

            BoundsInt room1 = new BoundsInt(
                room.min, 
                new Vector3Int(xSplit, room.size.y, room.size.z));
            BoundsInt room2 = new BoundsInt(
                new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
                new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
            
            roomsQueue.Enqueue(room1);
            roomsQueue.Enqueue(room2);
        }

        private void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room) {
            var ySplit = Random.Range(1, room.size.z);

            BoundsInt room1 = new BoundsInt(
                room.min, 
                new Vector3Int(room.size.x, room.size.y, ySplit)
            );
            BoundsInt room2 = new BoundsInt(
                new Vector3Int(room.min.x, room.min.y, room.min.z + ySplit),
                new Vector3Int(room.size.x, room.size.y, room.size.z - ySplit)
            );
            
            roomsQueue.Enqueue(room1);
            roomsQueue.Enqueue(room2);
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
    
        private void OnDrawGizmos() {
            Vector3 Pos1 = new Vector3(StartPosition.x, 0, StartPosition.y);
            Vector3 Pos2 = new Vector3(StartPosition.x, 0, dungeonDimensions.y);
            Vector3 Pos3 = new Vector3(dungeonDimensions.x, 0, StartPosition.y);
            Vector3 Pos4 = new Vector3(dungeonDimensions.x, 0, dungeonDimensions.y);

            Gizmos.DrawLine(Pos1, Pos2);
            Gizmos.DrawLine(Pos2, Pos4);
            Gizmos.DrawLine(Pos1, Pos3);
            Gizmos.DrawLine(Pos3, Pos4);


        }
    }
}