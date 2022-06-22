using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Richie.TowerDefence
{
    public class PlaceTower : MonoBehaviour
    {
        public GameObject Selected;
        public float offSet = 0.55f;

        private GameManager _gameManager;

        public event Cancel OnCancel;
        public delegate void Cancel();

        private void Start()
        {
            _gameManager = GetComponent<GameManager>();     
        }

        public GameObject PlantTower(Transform tile)
        {
            Vector3 tilePosition = new(tile.position.x, tile.position.y + offSet, tile.position.z);

            GameObject tower = Instantiate(Selected, tilePosition, Quaternion.identity, tile);
            tile.GetComponent<TileInfo>().Tower = tower;
            _gameManager._currentMoney -= tower.GetComponent<TowerBase>().Cost;

            Selected = null;
            return tower;
        }

        public void CancelSelect()
        {
            Selected = null;
            OnCancel?.Invoke();
        }
    }
}
