using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

namespace UnityIntro.Erik.AnimalSandbox
{
    [DefaultExecutionOrder(-50)]
    public class AnimalSandbox_GameManager : MonoBehaviour
    {
        #region Singleton
        public static AnimalSandbox_GameManager Instance { get; protected set; }
        private void Awake() {
            if(Instance != null){
                Destroy(this.gameObject);
            }
            Instance = this;
        }
        #endregion

        /// <summary>
        /// Time for Animations to execute
        /// </summary>
        public float AnimationTime = 1;


        [Header("Generation Settings")]
        public Vector2Int mapDimensions;
        public List<GameObject> TilePrefabs;
        public float TileScale = 10;

        [Header("References")]
        public List<AnimalSandbox_TileManager> Tiles = new();
        [Header("Animal Prefab references")]
        public GameObject Rabbit;

        #region  Map Related functions
        [ContextMenu("Generate Map")] public void generateHexagonalMap(){
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
        [ContextMenu("Destroy Map")] public void deleteMap(){
            foreach (var item in Tiles){
                DestroyImmediate(item);
            }
            Tiles = new();
        }
        #endregion
    
         [ContextMenu("Tick")] public void doTick(){
            foreach (var tile in Tiles)
            {
                tile.doTick();
            }
        }

        #region Animal Summoning functions
        public void summonAnimal(GameObject AnimalPrefab,AnimalSandbox_TileManager tm){
            AnimalSandbox_AnimalManager t = Instantiate(AnimalPrefab, transform.position, Quaternion.identity).GetComponent<AnimalSandbox_AnimalManager>();
            t.RegisterWithTile(tm);
        }
        #endregion
    }
}
