using UnityEngine;
using TMPro;

namespace Richie.TowerDefence
{
    public class InfoSelect : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject _currentSelect;
        [SerializeField] private PlaceTower _placeTower;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _damage;
        [SerializeField] private TextMeshProUGUI _range;
        [SerializeField] private TextMeshProUGUI _firerate;

        private TowerBase _tower;

        void Start()
        {
            _currentSelect.SetActive(false);
        }

        void Update()
        {
            if (_placeTower.Selected != null)
            {
                _currentSelect.SetActive(true);
                _tower = _placeTower.Selected.GetComponent<TowerBase>();

                _name.text = _tower.Name;
                _damage.text = $"Damage: {_tower.Damage}";
                _range.text = $"Range: {_tower.Range}";
                _firerate.text = $"Firerate: {_tower.FireRate}";
            }
            else _currentSelect.SetActive(false);
        }
    }
}
