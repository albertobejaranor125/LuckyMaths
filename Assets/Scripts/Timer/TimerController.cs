using TMPro;
using UnityEngine;

namespace LuckyMaths.Scripts.Timer
{
    public class TimerController : MonoBehaviour
    {
        #region Variables
        [SerializeField] private float _timeRemaining = 60f;
        [SerializeField] private TextMeshProUGUI _timerText;
        public static TimerController Instance;
        public bool IsPlaying;
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
            IsPlaying = false;
        }
        void Update()
        {
            if (!IsPlaying) return;
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
            }
            else if (_timeRemaining <= 0)
            {
                _timeRemaining = 0;
                IsPlaying = false;
                Debug.Log("Tiempo acabado");
            }
            int minutes = Mathf.FloorToInt(_timeRemaining / 60);
            int seconds = Mathf.FloorToInt(_timeRemaining % 60);
            _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        #endregion
        #region Methods
        public void StartTimer()
        {
            if (_timeRemaining <= 0) return;
            IsPlaying = true;
        }
        public void StopTimer()
        {
            IsPlaying = false;
        }
        #endregion
    }
}