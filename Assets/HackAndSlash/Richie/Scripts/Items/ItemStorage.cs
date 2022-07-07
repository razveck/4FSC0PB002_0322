using UnityEngine;

namespace Richie
{
    public class ItemStorage : MonoBehaviour
    {
        [SerializeField]
        private SaveSystem _saveSystem;
        public BagData ItemInfo = new();

        public event SaveState OnSave;
        public delegate void SaveState();

        public event LoadState OnLoad;
        public delegate void LoadState();

        private void Start()
        {
            _saveSystem.OnSave += SaveSystem_OnSave;
            _saveSystem.OnLoad += SaveSystem_OnLoad;

            if (SaveSystem.loadFromSave) Load();
        }

        private void SaveSystem_OnSave()
        {
            Save();
        }
        private void SaveSystem_OnLoad()
        {
            Load();
        }

        public void Save()
        {
            OnSave?.Invoke();
            SaveSystem.SaveBag(ItemInfo);
        }

        public void Load()
        {
            BagData data = SaveSystem.LoadBag() as BagData;
            ItemInfo = data;
            OnLoad?.Invoke();
        }
    }
}
