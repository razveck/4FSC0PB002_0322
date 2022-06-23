using UnityEngine;
using TMPro;

namespace Richie.TowerDefence
{
    public class SlotBase : MonoBehaviour
    {
        [Header("Tower Prefab")]
        [SerializeField] private GameObject _tower;
        private TowerBase _towerInfo;

        [Header("References")]
        [SerializeField] private PlaceTower _placeTower;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _cost;

        private void OnValidate()
        {
            _towerInfo = _tower.GetComponent<TowerBase>();

            _name.text = _towerInfo.Name;
            _cost.text = $"${_towerInfo.Cost}";
        }

        public void TowerSelect()
        {
            _placeTower.Selected = _tower;
        }
    }
}
