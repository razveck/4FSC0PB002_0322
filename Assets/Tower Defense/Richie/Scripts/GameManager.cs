using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

namespace Richie.TowerDefence
{
    public class GameManager : MonoBehaviour
    {
        [Header("Game Settings")]
        [SerializeField] private int _moneyOnStart = 100;
        [SerializeField] private int _rewardOnKill = 5;
        [SerializeField] private int _maxRounds = 10;
        [SerializeField] private int _maxLives = 3;
        private int _currentLives, _currentRound = 1;
        internal int currentMoney, sellAmount;
        internal bool refund;

        [Header("Text References")]
        [SerializeField] private TextMeshProUGUI _wallet;
        [SerializeField] private TextMeshProUGUI _lives;
        [SerializeField] private TextMeshProUGUI _round;
        [SerializeField] private TextMeshProUGUI _deathRounds;

        [Header("Object References")]
        [SerializeField] internal GameObject _showRefund;
        [SerializeField] private GameObject _loseScreen;
        [SerializeField] private GameObject _winScreen;
        [SerializeField] private GameObject _pauseMenu;

        private EnemySpawner enemyManager;
        private Interactions interactions;
        private PlayerInput _input;

        private bool _show;
        public static bool DisableHover = false;
        public static bool IsPaused = false;

        private void OnValidate()
        {
            _currentLives = _maxLives;
            currentMoney = _moneyOnStart;
            _wallet.text = $"${currentMoney}";
        }

        private void Awake()
        {
            _input = new PlayerInput(); 
        }

        void Start()
        {
            enemyManager = GetComponent<EnemySpawner>();
            interactions = GetComponent<Interactions>();

            enemyManager.OnEnemyExit += EnemyManager_OnEnemyExit;
            enemyManager.OnEnemyDeath += EnemyManager_OnEnemyDeath;
            enemyManager.OnChangeRound += EnemyManager_OnChangeRound;
            enemyManager.OnRoundOver += EnemyManager_OnRoundOver;
            interactions.OnSellTower += Interactions_OnSellTower;

            _input.defence.esc.performed += ctx => PauseGame();

            _winScreen.SetActive(false);
            _loseScreen.SetActive(false);
            _pauseMenu.SetActive(false);
        }

        public void Update()
        {
            _wallet.text = $"${currentMoney}";
        }

        private void Interactions_OnSellTower()
        {
            currentMoney += sellAmount;
            _showRefund.SetActive(false);
        }

        private void EnemyManager_OnChangeRound()
        {
            _currentRound++;
            enemyManager.start = true;
            enemyManager.stop = false;
            _round.text = $"{_currentRound}";
        }

        private void EnemyManager_OnEnemyDeath()
        { 
            currentMoney += _rewardOnKill;
            if (_currentLives <= 0)
            {
                _loseScreen.SetActive(true);
                _deathRounds.text = $"You lasted {_currentRound} round(s)";
            }
        }

        private void EnemyManager_OnEnemyExit()
        {
            _currentLives--;
            currentMoney -= _rewardOnKill;
            _lives.text = $"{_currentLives}";
        }

        private void EnemyManager_OnRoundOver()
        {
            if (_currentRound >= _maxRounds) _winScreen.SetActive(true);
        }

        private void PauseGame()
        {
            if (IsPaused && !_pauseMenu.activeInHierarchy) return;

            if (!_show)
            {
                IsPaused = true;
                DisableHover = true;

                _pauseMenu.SetActive(true);
                Time.timeScale = 0f;
                _show = true;
            }
            else
            {
                IsPaused = false;
                DisableHover = false;

                _pauseMenu.SetActive(false);
                Time.timeScale = 1f;
                _show = false;
            }
        }

        public void DefaultSettings()
        {
            IsPaused = false;
            DisableHover = false;
            Time.timeScale = 1f;
        }

        public void DisableTileInteract() => DisableHover = true;

        public void EnableTileInteract() => DisableHover = false;

        public void ReloadScene() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        private void OnEnable() =>_input.Enable();

        private void OnDisable() =>_input.Disable();
    }
}
