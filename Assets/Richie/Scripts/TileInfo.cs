using UnityEngine;
using System.Collections.Generic;

namespace Richie.TowerDefence
{
    public class TileInfo : MonoBehaviour
    {
        public Vector3 Coordinates { get; private set; }
        public bool IsPathTile { get; private set; }
        public bool IsValid { get; private set; }

        public GameObject Tower;
        public List<EnemyMovement> Occupants;

        private void Start()
        {
            SetValid(true);
        }

        private void Update()
        {
            CheckOccupants();
        }

        private void CheckOccupants()
        {
            if (Occupants.Count == 0) return;

            foreach (var item in Occupants)
            {
                if (item == null || new Vector3(item.Position.x, item.Position.z) != Coordinates)
                {
                    Occupants.Remove(item);
                    return;
                }
            }
        }

        public void SetValid(bool isValid) => IsValid = isValid;

        public void SetPathTile(bool isPathTile) => IsPathTile = isPathTile;

        public void SetCoords(Vector2 position) => Coordinates = position;
    }
}
