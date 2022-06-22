using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Richie.TowerDefence
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game Settings")]
        [SerializeField] private int moneyOnStart = 100;
        [SerializeField] private int rewardOnKill = 5;
        [SerializeField] private int maxLives = 3;
        private int _currentLives, _currentRound = 1;
        internal int _currentMoney;

        [Header("Text References")]
        [SerializeField] private TextMeshProUGUI _wallet;
        [SerializeField] private TextMeshProUGUI _lives;
        [SerializeField] private TextMeshProUGUI _round;
        [SerializeField] private TextMeshProUGUI _deathRounds;

        [Header("Object References")]
        [SerializeField] internal GameObject _refundActive;
        [SerializeField] private GameObject _deathScreen;

        internal int _sellAmount;
        internal bool _refund;

        private void OnValidate()
        {
            _currentLives = maxLives;
            _currentMoney = moneyOnStart;
        }

        void Start()
        {
            EnemySpawner enemyManager = GetComponent<EnemySpawner>();
            Interactions interactions = GetComponent<Interactions>();

            enemyManager.OnEnemyExit += EnemyManager_OnEnemyExit;
            enemyManager.OnEnemyDeath += EnemyManager_OnEnemyDeath;
            enemyManager.OnChangeRound += EnemyManager_OnChangeRound;
            interactions.OnSellTower += Interactions_OnSellTower;

            _deathScreen.SetActive(false);
        }

        public void Update()
        {
            _wallet.text = $"${_currentMoney}";
        }

        private void Interactions_OnSellTower()
        {
            _currentMoney += _sellAmount;
            _refundActive.SetActive(false);
        }

        private void EnemyManager_OnChangeRound()
        {
            _currentRound++;
            _round.text = $"{_currentRound}";
        }

        private void EnemyManager_OnEnemyDeath()
        { 
            _currentMoney += rewardOnKill;
            if (_currentLives <= 0)
            {
                _deathScreen.SetActive(true);
                _deathRounds.text = $"You lasted {_currentRound} round(s)";
            }
        }

        private void EnemyManager_OnEnemyExit()
        {
            _currentLives--;
            _currentMoney -= rewardOnKill;
            _lives.text = $"{_currentLives}";
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
