using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityIntro.Erik.TowerDefense
{
    public class TowerDefense_GameManager : MonoBehaviour
    {
        [Header("Combat")]
        public int PlayerHealth;
        public int EnemyCount;

        public List<TowerDefense_Enemy> Enemies;
        public List<TowerDefense_Tower> Towers;
        [Header("References")]
        public TowerDefense_MapManager MapGenerator;
        public GameObject EnemyPrefab;


        [ContextMenu("Auto Setup")]
        void AutoSetup(){
            TowerDefense_Tile tile;
            int i = -1;
            //determine start tile
            do
            {
                i++;
                tile = MapGenerator.Tiles[i];
            } while (tile.tileState == TileState.Impassable);
            MapGenerator.startTile = tile;

            i = MapGenerator.Tiles.Count;
            //determine end tile
            do
            {
                i--;
                tile = MapGenerator.Tiles[i];
            } while (tile.tileState == TileState.Impassable);
            MapGenerator.endTile = tile;
        }
        void doEnemies(){
            foreach (var enemy in Enemies){
                if(enemy.Health <= 0){
                    Enemies.Remove(enemy);
                    Destroy(enemy.gameObject);
                    continue;
                }
                if(enemy.currentTile == MapGenerator.endTile){
                    PlayerHealth--;
                    Destroy(enemy.gameObject);
                }
                
                int i = 0;
                while(MapGenerator.fullPath[i] != enemy.currentTile)
                    i++;
                
                enemy.currentTile = MapGenerator.fullPath[i++];
                enemy.transform.position = MapGenerator.fullPath[i++].transform.position;
                
            }
            if(Enemies.Count < EnemyCount){
                GameObject enemy = Instantiate(EnemyPrefab, MapGenerator.startTile.transform.position, Quaternion.identity);
                Enemies.Add(enemy.GetComponent<TowerDefense_Enemy>());
            }
        }
        void doTowers(){
            foreach (var tower in Towers){
                foreach (var enemy in Enemies)
                    if(Vector3.Distance(tower.transform.position, enemy.transform.position) < tower.Range){
                        enemy.Health-= tower.damage;
                    }
            }
        }
    
        void doTick(){
            doEnemies();
            doTowers();
        }
    }
}
