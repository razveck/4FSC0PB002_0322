using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityIntro.Erik.AnimalSimulation
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        public static GameManager Instance { get; protected set; }
        private void Awake(){
            if (Instance != null)
                Destroy(this.gameObject);
            Instance = this;
        }
        #endregion

        #region Map Generation
        [Header("Generation Settings")]
        public Vector2Int mapDimensions = new Vector2Int(5,5);
        public int InitialRabbits = 5;
        public int InitialFoxes = 3; 
        public int InitialBears = 1;
        public int InitialHawk = 3;
        [Space(), Header("Generation Prefabs")]
        public List<GameObject> TilePrefabs;
        public GameObject RabbitPrefab;
        public GameObject FoxPrefab;

        [Space(), Header("Misc gen info")]
        public float AnimationTime = 1;
        public float TileScale;
        public List<TileManager> Tiles; 
        public AnimalStats animalStats;

        [ContextMenu("Generate Map")]
        public void generateHexagonalMap()
        {
            GameObject temp;
            for (int x = 0; x < mapDimensions.x; x++)
            {
                for (int z = 0; z < mapDimensions.y; z++)
                {
                    float zOffset = (x % 2 == 0) ? 0 : -0.5f;
                    Vector3 pos = new Vector3(x * TileScale, 0, (zOffset - z) * TileScale);
                    temp = Instantiate(TilePrefabs[Random.Range(0, TilePrefabs.Count)], pos, Quaternion.identity, transform);
                    Tiles.Add(temp.GetComponent<TileManager>());
                }
            }
        }
        [ContextMenu("Generate Animals")]
        public void spawnAnimals()
        {
            for (int i = 0; i < InitialRabbits; i++){
                summonAnimal(RabbitPrefab, Tiles[Mathf.RoundToInt(Random.Range(0, Tiles.Count))]);
            }
            for (int i = 0; i < InitialFoxes; i++){
                summonAnimal(FoxPrefab, Tiles[Mathf.RoundToInt(Random.Range(0, Tiles.Count))]);
            }
            
        }

        #endregion

        #region Game Logic 
        [ContextMenu("Tick")] 
        public void doTick(){
            foreach (var tile in Tiles)
                tile.Tick();
        }
        public void summonAnimal(GameObject AnimalPrefab,TileManager tm, int count = 0){
            AnimalManager t = Instantiate(AnimalPrefab, transform.position, Quaternion.identity).GetComponent<AnimalManager>();
            t.Count = count;
            t.RegisterWithTile(tm);
        }
        #endregion

        #region Savegame Logic
        [ContextMenu("Save Game")]
        public void SaveGame(){
            SaveGameManager.SaveAnimals();
        }

        [ContextMenu("Load Game")]
        public void LoadGame(){
            List<AnimalSave> animalSaves = SaveGameManager.LoadAnimals();
            foreach (var AnimalSave in animalSaves){
                switch (AnimalSave.ID)
                {
                    case 0 : 
                        summonAnimal(RabbitPrefab, Tiles[AnimalSave.Position], AnimalSave.Count);
                        break;
                    case 1 : 
                        summonAnimal(FoxPrefab, Tiles[AnimalSave.Position], AnimalSave.Count);
                        break;
                    default:
                        break;
                }
            }

        }
        #endregion
    }
}