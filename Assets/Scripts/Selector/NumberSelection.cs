using LuckyMaths.Scripts.Display;
using LuckyMaths.Scripts.Manager;
using System.Collections;
using UnityEngine;

namespace LuckyMaths.Scripts.Selector
{
    public class NumberSelection : MonoBehaviour
    {
        #region Variables
        private OperationDisplay _operationDisplay;
        private int _currentSelection;
        private int[] _selectedNumbers = new int[3];
        private GeneratorManager _generatorManager;
        private bool _firstAttempt;
        private const int BASE_SCORE = 100;
        private bool _isProcessing;
        private Color32 _incorrectColor = new Color32(255, 60, 172, 255);
        private Color32 _correctColor = new Color32(55, 255, 136, 255);
        private const float FEEDBACK_DURATION = 0.3f;
        #endregion
        #region Unity Messages
        void Start()
        {
            _operationDisplay = FindAnyObjectByType<OperationDisplay>();
            _generatorManager = FindAnyObjectByType<GeneratorManager>();
            _currentSelection = 0;
            _firstAttempt = true;
            _isProcessing = false;
        }
        #endregion
        #region Methods
        public void SelectNumber(int button)
        {
            if (_isProcessing) return;
            if (GameManager.Instance.GetCurrentGameState() != GameState.Playing) return;
            if (_currentSelection >= 3) return;
            int _numberSelected = _operationDisplay.GetButtonNumber(button);
            if (_numberSelected == -1) return;
            _selectedNumbers[_currentSelection] = _numberSelected;
            _operationDisplay.UpdateNumberSlot(_currentSelection + 1, _numberSelected);
            _operationDisplay.SetButtonInteractable(button, false);
            _currentSelection++;
            if(_currentSelection == 3)
            {
                bool _isCorrect = _generatorManager.ValidatePlayerOperation(_selectedNumbers);
                if (_isCorrect)
                {
                    Debug.Log("Correct Operation!!\n");
                    ScoreManager.Instance.AddScore(BASE_SCORE);
                    if (_firstAttempt)
                    {
                        ScoreManager.Instance.IncreaseCombo();
                    }
                    if (_generatorManager.GenerateAndValidateExpression())
                    {
                        /*ResetSelection();
                        _firstAttempt = true;
                        _operationDisplay.RefreshRound();*/
                        StartCoroutine(CorrectAnswerFeedback());
                    }
                    else
                    {
                        Debug.LogError("It can't generate the next round\n");
                        GameManager.Instance.EndGame();
                    }
                }
                else
                {
                    Debug.Log("Incorrect Operation\n");
                    _firstAttempt = false;
                    ScoreManager.Instance.ResetCombo();
                    //ResetSelection();
                    StartCoroutine(IncorrectAnswerFeedback());
                }
            }
        }
        private IEnumerator IncorrectAnswerFeedback()
        {
            _isProcessing = true;
            _operationDisplay.SetSelectedNumbersColor(_incorrectColor);
            yield return new WaitForSeconds(FEEDBACK_DURATION);
            _operationDisplay.ResetSelectedNumbersColor();
            ResetSelection();
            _isProcessing = false;
        }
        private IEnumerator CorrectAnswerFeedback()
        {
            _isProcessing = true;
            _operationDisplay.SetSelectedNumbersColor(_correctColor);
            yield return new WaitForSeconds(FEEDBACK_DURATION);
            _operationDisplay.ResetSelectedNumbersColor();
            ResetSelection();
            _firstAttempt = true;
            _operationDisplay.RefreshRound();
            _isProcessing = false;
        }
        private void ResetSelection()
        {
            for(int i = 0; i < 3; i++)
            {
                _selectedNumbers[i] = 0;
            }
            for(int i = 0; i < 6; i++)
            {
                _operationDisplay.SetButtonInteractable(i, true);
            }
            _operationDisplay.ClearNumberSlots();
            _currentSelection = 0;
        }
        
        #endregion
    }
}