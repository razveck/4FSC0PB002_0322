using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Timeline;

namespace UnityIntro.Erik.TowerDefense
{
    public class TowerDefense_MapGenerator : MonoBehaviour
    {
        [Header("Map Parameters")]
        public Vector2Int mapSize;
        public GameObject tilePrefab;
        public List<TowerDefense_Tile> Tiles;


        [ContextMenu("Generate Hexagonal Map")]
        public void generateHexagonalMap(){
            GameObject temp;
            for (int x = 0; x < mapSize.x; x++)
                for (int y = 0; y < mapSize.y; y++)
                {
                    float zOffset = (x % 2 == 0) ? 0 : -0.5f;
                    Vector3 pos = new Vector3(x, 0, zOffset - y);
                    temp = Instantiate(tilePrefab, pos, new Quaternion());
                    temp.transform.parent = this.transform;
                    Tiles.Add(temp.GetComponent<TowerDefense_Tile>());
                    temp.GetComponent<TowerDefense_Tile>().generator = this;
                }

            foreach (var item in Tiles)
            {
                item.CreateConnections();
            }
        }

        /// <summary>
        /// A* based path generation
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void generatePath(TowerDefense_Tile start, TowerDefense_Tile end){
            start.tileState = TileState.PathStart;
            end.tileState = TileState.PathEnd;

            //outermost edge
            Queue<TowerDefense_Tile> frontier = new();
            frontier.Enqueue(start);
        
            HashSet<TowerDefense_Tile> reached = new();
            reached.Add(start);

            int cost = 0;
            while (frontier.Count > 0){
                var current = frontier.Dequeue();

                checkTile(current.connection_North);
                checkTile(current.connection_NorthWest);
                checkTile(current.connection_NorthEast);
                checkTile(current.connection_South);
                checkTile(current.connection_SouthWest);
                checkTile(current.connection_SouthEast);



                void checkTile(TowerDefense_Tile tile){
                    if (!reached.Contains(tile))
                    {
                        frontier.Enqueue(tile);
                        reached.Add(tile);
                    }

                }
            }
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            for (int x = 0; x < mapSize.x; x++)
                for (int y = 0; y < mapSize.y; y++){
                    float zOffset = (x % 2 == 0) ? 0 : -0.5f;
                    Vector3 pos = new Vector3(x, 0, zOffset - y);

                    Gizmos.DrawSphere(pos, .1f);
                }
        }
    }
}
