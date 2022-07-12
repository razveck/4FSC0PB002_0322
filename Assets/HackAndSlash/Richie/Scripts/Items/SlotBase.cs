using UnityEngine;
using TMPro;

namespace Richie
{
    public class SlotBase : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private ItemStorage _storage;
        [SerializeField] private PlayerBag _playerBag;
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private Items.Item _type;
        private int _count = 0;

        private void Start()
        {
            _countText.text = $"{_count}";

            _storage.OnSave += Storage_OnSave;
            _storage.OnLoad += Storage_OnLoad;

            _playerBag.OnCollect += PlayerBag_OnCollect;
        }

        private void Storage_OnLoad()
        {
            Load();
        }

        private void Storage_OnSave()
        {
            Save();
        }

        private void PlayerBag_OnCollect(Items.Item item)
        {
            if (_type != item) return;
            _countText.text = $"{++_count}";
        }
        public void Save()
        {
            ItemData data = new()
            {
                Count = _count,
                Type = _type
            };

            if (_storage.ItemInfo.Items.Count <= 0) 
                _storage.ItemInfo.Items.Add(data);

            for (int i = 0; i < _storage.ItemInfo.Items.Count; i++)
            {
                if (_storage.ItemInfo.Items[i].Type == _type)
                {
                    _storage.ItemInfo.Items[i] = data;
                    return;
                }

                if (i == _storage.ItemInfo.Items.Count - 1 && _storage.ItemInfo.Items[i].Type != _type)
                    _storage.ItemInfo.Items.Add(data);
            }
        }

        public void Load()
        {
            for (int i = 0; i < _storage.ItemInfo.Items.Count; i++)
            {
                if (_storage.ItemInfo.Items[i].Type == _type)
                {
                    _count = _storage.ItemInfo.Items[i].Count;
                    _countText.text = $"{_count}";
                    return;
                }
            }
        }
        
    }
}
