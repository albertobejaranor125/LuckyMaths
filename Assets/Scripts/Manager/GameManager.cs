using LuckyMaths.Scripts.Timer;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LuckyMaths.Scripts.Manager
{
    public enum GameState
    {
        Playing,
        GameOver
    }
    public class GameManager : MonoBehaviour
    {
        #region Variables
        public static GameManager Instance;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private GameObject _scorePanel;
        [SerializeField] private GameObject _timerPanel;
        [SerializeField] private GameObject _operationPanel;
        [SerializeField] private GameObject _numberPanel;
        private GameState _gameState;
        #endregion
        #region Unity Messages
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            Debug.Log("Instance created GameManger");
        }
        void Start()
        {
            _gameOverPanel.SetActive(false);
            StartGame();
        }
        #endregion
        #region Methods
        public void StartGame()
        {
            _gameState = GameState.Playing;
            TimerController.Instance.StartTimer();
            _scorePanel.SetActive(true);
            _timerPanel.SetActive(true);
            _operationPanel.SetActive(true);
            _numberPanel.SetActive(true);
            _gameOverPanel.SetActive(false);
        }
        public void EndGame()
        {
            if (_gameState != GameState.Playing) return;
            _gameState = GameState.GameOver;
            TimerController.Instance.StopTimer();
            _scorePanel.SetActive(false);
            _timerPanel.SetActive(false);
            _operationPanel.SetActive(false);
            _numberPanel.SetActive(false);
            _gameOverPanel.SetActive(true);
        }
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public GameState GetCurrentGameState() 
        { 
            return _gameState; 
        }
        #endregion
    }
}