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

        [Header("References")]
        public TowerDefense_MapManager generator;
        public List<TowerDefense_Tile> Adjacencies = new();

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

        public void CreateConnections()
        {
            foreach (TowerDefense_Tile Tile in generator.Tiles)
            {
                if (Tile == this)
                    continue;
                if (Vector3.Distance(transform.position, Tile.transform.position) < 1.75f)
                    Adjacencies.Add(Tile);
            }
        }
    }
}
