using TMPro;
using UnityEngine;

namespace LuckyMaths.Scripts.Manager
{
    public class ScoreManager : MonoBehaviour
    {
        #region Variables
        public int Score { get; private set; }
        public static ScoreManager Instance;
        [SerializeField] private TextMeshProUGUI _scoreText;
        private int _comboMultiplier;
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
        }
        void Start()
        {
            _comboMultiplier = 1;
            Score = 0;
            _scoreText.fontStyle = (FontStyles) FontStyle.BoldAndItalic;
            _scoreText.fontSize = 54;
            _scoreText.text = "Score: " + Score;
        }
        #endregion
        #region Methods
        public void AddScore(int amount)
        {
            if (amount <= 0) return;
            int finalAmount = amount * _comboMultiplier;
            Score += finalAmount;
            _scoreText.fontStyle = (FontStyles)FontStyle.BoldAndItalic;
            _scoreText.fontSize = 54;
            _scoreText.text = "Score: " + Score;
        }
        public void ResetCombo()
        {
            _comboMultiplier = 1;
        }
        public void IncreaseCombo()
        {
            _comboMultiplier++;
        }
        public int GetComboMultiplier()
        {
            return _comboMultiplier;
        }
        #endregion
    }
}