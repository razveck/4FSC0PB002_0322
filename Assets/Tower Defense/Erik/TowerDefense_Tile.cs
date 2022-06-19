using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace UnityIntro.Erik.TowerDefense
{
    public enum TileState{
        Nothing,
        PathStart,
        PathEnd,
        PathTile,
        Placable,
        Impassable
    }

    public class TowerDefense_Tile : MonoBehaviour
    {

        [Header("Tile Info")]
        public TileState tileState = TileState.Nothing;
        [SerializeField] private Material unsetTile;
        [SerializeField] private Material startTile;
        [SerializeField] private Material endTile;
        [SerializeField] private Material pathTile;
        [SerializeField] private Material placableTile;
        [SerializeField] private Material impassableTile;

        private void Update() {
            MeshRenderer mr = GetComponent<MeshRenderer>(); 
            switch (tileState)
            {
                case TileState.Nothing :
                    mr.material = unsetTile;
                    break;
                case TileState.PathStart :
                    mr.material = startTile;
                    break;
                case TileState.PathEnd :
                    mr.material = endTile;
                    break;
                case TileState.PathTile :
                    mr.material = pathTile;
                    break;
                case TileState.Placable :
                    mr.material = placableTile;
                    break;
                case TileState.Impassable :
                    mr.material = impassableTile;
                    break;
                
            }
        }
        [Header("References")]
        public TowerDefense_MapGenerator generator;

        public TowerDefense_Tile connection_North;
        public TowerDefense_Tile connection_NorthWest;
        public TowerDefense_Tile connection_NorthEast;
        public TowerDefense_Tile connection_South;
        public TowerDefense_Tile connection_SouthWest;
        public TowerDefense_Tile connection_SouthEast;

        [ContextMenu("Create Connections")]
        public void CreateConnections(){
            foreach (TowerDefense_Tile Tile in generator.Tiles){
                if(Tile == this)
                    continue;
                if(Vector3.Distance(transform.position,Tile.transform.position) < 1.75f){
                    Vector3 direction = (Tile.transform.position - transform.position);

                    Debug.DrawLine(transform.position, Tile.transform.position, Color.green, 1);
                    Debug.Log($"{(Tile.transform.position - transform.position)}");



                    if(direction == new Vector3(0,0,1)){
                        connection_North = Tile;
                    }
                    if(direction == new Vector3(1, 0, .5f)){
                        connection_NorthWest = Tile;
                    }
                    if(direction == new Vector3(-1, 0, .5f)){
                        connection_NorthEast = Tile;
                    }
                    if(direction == new Vector3(0,0,-1)){
                        connection_South = Tile;
                    }
                    if(direction == new Vector3(1, 0, -.5f)){
                        connection_SouthWest = Tile;
                    }
                    if(direction == new Vector3(-1, 0, -.5f)){
                        connection_SouthEast = Tile;
                    }
                }
            }
        }




        private void OnDrawGizmos() {
            Gizmos.color = Color.black;
            if(connection_North != null){
                Gizmos.DrawLine(transform.position, connection_North.transform.position);
            }
            if(connection_NorthWest != null){
                Gizmos.DrawLine(transform.position, connection_NorthWest.transform.position);
            }
            if(connection_NorthEast != null){
                Gizmos.DrawLine(transform.position, connection_NorthEast.transform.position);
            }
            if(connection_South != null){
                Gizmos.DrawLine(transform.position, connection_South.transform.position);
            }
            if(connection_SouthWest != null){
                Gizmos.DrawLine(transform.position, connection_SouthWest.transform.position);
            }
            if(connection_SouthEast != null){
                Gizmos.DrawLine(transform.position, connection_SouthEast.transform.position);
            }
        }
    }
}
