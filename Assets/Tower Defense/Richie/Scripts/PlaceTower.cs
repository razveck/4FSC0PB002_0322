using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Richie.TowerDefence
{
    public class PlaceTower : MonoBehaviour
    {
        private GameManager _gameManager;

        [SerializeField] private float _yOffset = 0.55f;
        internal GameObject Selected;
        
        public event Cancel OnCancel;
        public delegate void Cancel();

        private void Start()
        {
            _gameManager = GetComponent<GameManager>();     
        }

        public GameObject PlantTower(Transform tile)
        {
            Vector3 tilePosition = new(tile.position.x, tile.position.y + _yOffset, tile.position.z);

            GameObject tower = Instantiate(Selected, tilePosition, Quaternion.identity, tile);
            _gameManager._currentMoney -= tower.GetComponent<TowerBase>().Cost;
            tile.GetComponent<TileInfo>().SetTower(tower);

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
