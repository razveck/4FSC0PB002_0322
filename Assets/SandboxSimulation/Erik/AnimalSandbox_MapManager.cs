using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityIntro.Erik.AnimalSandbox
{
    public class AnimalSandbox_MapManager : MonoBehaviour{
        [Header("Generation Settings")]
        public Vector2Int mapDimensions;
        public List<GameObject> TilePrefabs;
        public float TileScale = 10;

        [Header("References")]
        public List<AnimalSandbox_TileManager> Tiles = new();

        [ContextMenu("Generate Map")]
        public void generateHexagonalMap(){
            GameObject temp;
            for (int x = 0; x < mapDimensions.x; x++){
                for (int z = 0; z < mapDimensions.y; z++){
                    float zOffset = (x % 2 == 0) ? 0 : -0.5f;
					Vector3 pos = new Vector3(x * TileScale, 0, (zOffset - z) * TileScale);
                    temp = Instantiate(TilePrefabs[Random.Range(0, TilePrefabs.Count)], pos, Quaternion.identity, transform);
                    Tiles.Add(temp.GetComponent<AnimalSandbox_TileManager>());
                }
            }
        }
        [ContextMenu("Destroy Map")]
        public void deleteMap(){
            foreach (var item in Tiles){
                DestroyImmediate(item);
            }
            Tiles = new();
        }
    }
}
