using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEditor.MemoryProfiler;
using UnityEngine;

namespace UnityIntro.Erik.AnimalSandbox
{
    public class AnimalSandbox_TileManager : MonoBehaviour
    {
        [Header("Stats")]
        public float RemainingFood = 10;
        public float FoodGrowth = 1;
        public float TraverseCost = 1;
        public float ConnectionDistance = 15;

        [Header("References")]
        public List<AnimalSandbox_TileManager> Adjacencies;
        public AnimalSandbox_GameManager Manager;
        public LayerMask TileLayer;

        private void Awake() {
            Manager = AnimalSandbox_GameManager.Instance;
        }

        private void OnEnable(){
            CreateConnections();
        }

        public void CreateConnections(){
            Collider[] tiles = Physics.OverlapSphere(transform.position, ConnectionDistance, TileLayer);
            foreach (var item in tiles)
                Adjacencies.Add(item.gameObject.GetComponent<AnimalSandbox_TileManager>());
        }
        private void OnDrawGizmosSelected() {
            Gizmos.DrawWireSphere(transform.position, ConnectionDistance);
        }
    }
}
