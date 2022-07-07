using UnityEngine;

namespace Richie
{
    public class PlayerStorage : MonoBehaviour
    {
        [SerializeField] 
        private SaveSystem _saveSystem;
        public PlayerData PlayerInfo = new();

        public event SaveState OnSave;
        public delegate void SaveState();

        public event LoadState OnLoad;
        public delegate void LoadState();

        private void Start()
        {
            _saveSystem.OnSave += SaveSystem_OnSave;
            _saveSystem.OnLoad += SaveSystem_OnLoad;
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
            SaveSystem.SavePlayer(PlayerInfo);
        }

        public void Load()
        {
            PlayerData data = SaveSystem.LoadPlayer() as PlayerData;
            PlayerInfo = data;
            OnLoad?.Invoke();
        }
    }
}
