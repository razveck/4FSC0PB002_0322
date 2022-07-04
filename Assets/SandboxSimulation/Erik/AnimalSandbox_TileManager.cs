using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Security;
using UnityEditor.MemoryProfiler;
using UnityEngine;

namespace UnityIntro.Erik.AnimalSandbox
{
    [SelectionBase()]
    public class AnimalSandbox_TileManager : MonoBehaviour
    {
        [Header("Stats")]
        public float RemainingFood = 10;
        public float FoodGrowth = 1;
        public float TraverseCost = 1;
        public float ConnectionDistance = 15;

        [Header("References")]
        public LayerMask TileLayer;
        public List<AnimalSandbox_TileManager> Adjacencies;
        public AnimalSandbox_GameManager Manager;

        public List<AnimalSandbox_AnimalManager> Animals;
        public List<Transform> AnimalPositions;
        private void OnEnable() {
            Initialize();
        }

        void Initialize(){
            Manager = AnimalSandbox_GameManager.Instance;
            CreateConnections();
        }
        void DoAnimalPositions(){
            for (int i = 0; i < Animals.Count; i++){
                Animals[i].transform.position = transform.position + new Vector3(0,0.5f,0);
            }
            for (int i = 0; i < AnimalPositions.Count; i++){
                Animals[i].transform.position = AnimalPositions[i].position;
            }
        }

        public void doTick(){
            foreach (var Animal in Animals){
                Animal.doTick();
            }
            DoAnimalPositions();
        }

        public void CreateConnections(){
            Collider[] tiles = Physics.OverlapSphere(transform.position, ConnectionDistance, TileLayer);
            foreach (var item in tiles)
                Adjacencies.Add(item.gameObject.GetComponent<AnimalSandbox_TileManager>());
        }
        private void OnDrawGizmosSelected() {
            //Gizmos.DrawWireSphere(transform.position, ConnectionDistance);
        }
    }
}
