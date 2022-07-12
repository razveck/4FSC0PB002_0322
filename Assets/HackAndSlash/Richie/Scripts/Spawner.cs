using System.Collections.Generic;
using UnityEngine;

namespace Richie
{
    public class Spawner : MonoBehaviour
    {
        private MapGenerator _map;
        private PlayerBag _playerBag;
        private List<Room> _rooms = new();
        internal GameObject currentPlayer;

        private PlayerData _savedPlayer = new();
        private List<GameObject> _allEnemies = new();
        private List<PlayerData> _savedEnemies = new();
        private PlayerStorage _playerStorage;
        private EnemyStorage _enemyStorage;

        [SerializeField] private GameObject _player;
        [SerializeField] private GameObject _enemies;
        [SerializeField] private int _maxEnemies = 10;
        [SerializeField] private Transform _itemContainer;
        [SerializeField] private GameObject _playerHealthbar;

        private void Start()
        {
            _playerStorage = GetComponent<PlayerStorage>();
            _enemyStorage = GetComponent<EnemyStorage>();
            _playerBag = GetComponent<PlayerBag>();
            _map = GetComponent<MapGenerator>();
            _rooms = _map.Rooms;

            _enemyStorage.OnSave += EnemyStorage_OnSave;
            _playerStorage.OnSave += PlayerStorage_OnSave;

            if (SaveSystem.loadFromSave)
            {
                LoadPlayer();
                LoadEnemies();
                SpawnPlayer(_savedPlayer);
                SpawnEnemies(_savedEnemies);
            }
            else
            {
                SpawnPlayer();
                SpawnEnemies();
            }

            _playerHealthbar.SetActive(true);
        }

        private void PlayerStorage_OnSave()
        {
            SavePlayer();
        }

        private void EnemyStorage_OnSave()
        {
            SaveEnemies();
        }

        private void SpawnPlayer()
        {
            currentPlayer = Instantiate(_player, FindPosition(), Quaternion.identity);
            currentPlayer.name = "Player";
        }

        private void SpawnPlayer(PlayerData player)
        {
            currentPlayer = Instantiate(_player, player.Origin, Quaternion.identity);
            currentPlayer.name = "Player";

            GameObject child = currentPlayer.GetComponentInChildren<PlayerHealth>().gameObject;
            child.GetComponent<PlayerHealth>().SetHealth(player.Health, player.MaxHealth);
            child.transform.localPosition = player.Position;
        }

        private void SpawnEnemies()
        {
            GameObject enemyList = new();
            enemyList.name = "Enemies";
            int roomCount = _rooms.Count;

            for (int i = 0; i < roomCount; i++)
            {
                Room room = FindRoom();
                int amount = Random.Range(1, _maxEnemies + 1);
                for (int e = 0; e < amount; e++)
                {
                    int xStart = Random.Range(room.Origin.x + 1, room.Origin.x + room.Range.x - 1);
                    int yStart = Random.Range(room.Origin.y + 1, room.Origin.y + room.Range.y - 1);

                    GameObject thisEnemy = Instantiate(_enemies, new Vector2(xStart, yStart), Quaternion.identity, enemyList.transform);
                    thisEnemy.GetComponent<EnemyMove>().Target = currentPlayer.GetComponentInChildren<PlayerMove>().transform;
                    thisEnemy.GetComponent<EnemyHealth>().container = _itemContainer;
                    thisEnemy.GetComponent<EnemyHealth>().playerBag = _playerBag;
                    _allEnemies.Add(thisEnemy);
                }
            }
        }

        private void SpawnEnemies(List<PlayerData> enemies)
        {
            GameObject enemyList = new();
            enemyList.name = "Enemies";

            for (int i = 0; i < enemies.Count; i++)
            {
                GameObject thisEnemy = Instantiate(_enemies, enemies[i].Position, Quaternion.identity, enemyList.transform);
                EnemyHealth health = thisEnemy.GetComponent<EnemyHealth>();
                EnemyMove move = thisEnemy.GetComponent<EnemyMove>();

                move.Target = currentPlayer.GetComponentInChildren<PlayerMove>().transform;
                health.SetHealth(enemies[i].Health, enemies[i].MaxHealth);
                move.SetOrigin(enemies[i].Origin);
                health.container = _itemContainer;
                health.playerBag = _playerBag;
                _allEnemies.Add(thisEnemy);
            }
        }

        private Room FindRoom()
        {
            Room room = _rooms[Random.Range(0, _rooms.Count)];
            _rooms.Remove(room);

            return room;
        }

        private Vector2 FindPosition()
        {
            Room room = _rooms[Random.Range(0, _rooms.Count)];

            int xStart = Random.Range(room.Origin.x + 1, room.Origin.x + room.Range.x - 1);
            int yStart = Random.Range(room.Origin.y + 1, room.Origin.y + room.Range.y - 1);
            _rooms.Remove(room);

            return new(xStart, yStart);
        }

        public void SavePlayer()
        {
            GameObject player = currentPlayer.GetComponentInChildren<PlayerHealth>().gameObject;

            _playerStorage.PlayerInfo = new()
            {
                Health = player.GetComponent<PlayerHealth>().GetHealth(),
                MaxHealth = player.GetComponent<PlayerHealth>().GetMaxHealth(),
                Position = player.transform.localPosition,
                Origin = currentPlayer.transform.position
            };
        }

        public void SaveEnemies()
        {
            List<PlayerData> data = new();
            for (int i = 0; i < _allEnemies.Count; i++)
            {
                if (_allEnemies[i] != null)
                {
                    PlayerData enemyInfo = new()
                    {
                        Health = _allEnemies[i].GetComponent<EnemyHealth>().GetHealth(),
                        MaxHealth = _allEnemies[i].GetComponent<EnemyHealth>().GetMaxHealth(),
                        Position = _allEnemies[i].transform.position,
                        Origin = _allEnemies[i].GetComponent<EnemyMove>().GetOrigin()
                    };

                    data.Add(enemyInfo);
                }
            }

            _enemyStorage.EnemyInfo.Enemies = data;
        }

        public void LoadEnemies()
        {
            EnemyData data = SaveSystem.LoadEnemies() as EnemyData;
            _savedEnemies = data.Enemies;
        }

        public void LoadPlayer()
        {
            PlayerData data = SaveSystem.LoadPlayer() as PlayerData;
            _savedPlayer = data;
        }
    }
}
