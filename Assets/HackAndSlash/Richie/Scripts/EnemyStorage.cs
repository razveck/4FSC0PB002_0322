using UnityEngine;

namespace Richie
{
    public class EnemyStorage : MonoBehaviour
    {
        [SerializeField]
        private SaveSystem _saveSystem;
        public EnemyData EnemyInfo = new();

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
            EnemyInfo.Enemies = new();
            OnSave?.Invoke();
            SaveSystem.SaveEnemies(EnemyInfo);
        }

        public void Load()
        {
            EnemyData data = SaveSystem.LoadEnemies() as EnemyData;
            EnemyInfo = data;
            OnLoad?.Invoke();
        }
    }
}
