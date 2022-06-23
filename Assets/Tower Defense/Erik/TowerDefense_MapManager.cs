using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityIntro.Erik.TowerDefense
{
    public class TowerDefense_MapManager : MonoBehaviour
    {
        [Header("Map Parameters")]
        public Vector2Int mapSize;
        public GameObject tilePrefab;
        public List<TowerDefense_Tile> Tiles;

        [Header("Path")]
        [SerializeField] private TowerDefense_Tile _startTile;
        public TowerDefense_Tile startTile{
            get {return _startTile;}
            set {
                if(_startTile != null){
                    _startTile.tileState = TileState.Nothing;
                }
                _startTile = value;
                _startTile.tileState = TileState.PathStart;
                if(startTile != null && endTile != null)
                    generatePath();
            }
        }
        [SerializeField] private TowerDefense_Tile _endTile;
        public TowerDefense_Tile endTile{
            get {return _endTile;}
            set {
                if(_endTile != null){
                    _endTile.tileState = TileState.Nothing;
                }
                _endTile = value;
                _endTile.tileState = TileState.PathEnd;
                if(startTile != null && endTile != null)
                    generatePath();
            }
        }
        public List<TowerDefense_Tile> fullPath;


        /// <summary>
        /// prepares a blanco map
        /// </summary>
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
                item.CreateConnections();
        }

        /// <summary>
        /// Generates the full path from start to end
        /// </summary>
        [ContextMenu("Generate Path")]
        public void generatePath(){
            //Outer reached edge
            PriorityQueue<TowerDefense_Tile> frontier = new PriorityQueue<TowerDefense_Tile>();
            frontier.Enqueue(startTile, 0);

            //From which tile this tile has been reached
            Dictionary<TowerDefense_Tile, TowerDefense_Tile> cameFrom = new();
            cameFrom[startTile] = null;

            //which cost you need to reach this tile
            Dictionary<TowerDefense_Tile, float> costSoFar = new();
            costSoFar[startTile] = 0;

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();
                if(current == endTile)
                    break;

                foreach (var next in current.Adjacencies)
                {
                    
                    //if(next.tileState == TileState.Impassable)
                    //    continue;
                    float newCost = costSoFar[current] + 1;
                    if(!costSoFar.ContainsKey(next) || newCost < costSoFar[next]){
                        costSoFar[next] = newCost;
                        frontier.Enqueue(next, newCost + Vector3.Distance(next.transform.position, endTile.transform.position));
                        cameFrom[next] = current;
                    }
                }
            }

            //construct the path
            var cur = endTile;
            List<TowerDefense_Tile> path = new();

            while(cur != startTile){
                path.Add(cur);
                cur = cameFrom[cur];
            }
            path.Add(startTile);
            path.Reverse();

            fullPath = path;
            foreach (var item in path)
                item.tileState = TileState.PathTile;
            startTile.tileState = TileState.PathStart;
            endTile.tileState = TileState.PathEnd;
            
        }
    }
}
