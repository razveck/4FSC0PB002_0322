using UnityEngine;
using System.IO;

namespace Richie
{
    public class SaveSystem : MonoBehaviour
    {
        public static bool loadFromSave;

        public event SaveState OnSave;
        public delegate void SaveState();

        public event LoadState OnLoad;
        public delegate void LoadState();

        public void Save()
        {
            OnSave?.Invoke();
        }

        public void New()
        {
            ReloadScene.Reload();
            loadFromSave = false;
        }

        public void Load()
        {
            ReloadScene.Reload();
            loadFromSave = true;
            OnLoad?.Invoke();
        }

        // MAP DATA //
        public static void SaveMap(object data)
        {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.dataPath + "/HackAndSlash/Richie/Saves/" + "/mapFile.json", json);
        }

        public static object LoadMap()
        {
            string json = File.ReadAllText(Application.dataPath + "/HackAndSlash/Richie/Saves/" + "/mapFile.json");
            object data = JsonUtility.FromJson<MapData>(json);

            return data;
        }

        // PLAYER DATA //
        public static void SavePlayer(object data)
        {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.dataPath + "/HackAndSlash/Richie/Saves/" + "/playerFile.json", json);
        }

        public static object LoadPlayer()
        {
            string json = File.ReadAllText(Application.dataPath + "/HackAndSlash/Richie/Saves/" + "/playerFile.json");
            object data = JsonUtility.FromJson<PlayerData>(json);

            return data;
        }

        // ENEMY DATA // 
        public static void SaveEnemies(object data)
        {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.dataPath + "/HackAndSlash/Richie/Saves/" + "/enemiesFile.json", json);
        }

        public static object LoadEnemies()
        {
            string json = File.ReadAllText(Application.dataPath + "/HackAndSlash/Richie/Saves/" + "/enemiesFile.json");
            object data = JsonUtility.FromJson<EnemyData>(json);

            return data;
        }

        // INVENTORY DATA //
        public static void SaveBag(object data)
        {
            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.dataPath + "/HackAndSlash/Richie/Saves/" + "/inventoryFile.json", json);
        }

        public static object LoadBag()
        {
            string json = File.ReadAllText(Application.dataPath + "/HackAndSlash/Richie/Saves/" + "/inventoryFile.json");
            object data = JsonUtility.FromJson<BagData>(json);

            return data;
        }
    }
}
