using LuckyMaths.Scripts.Display;
using LuckyMaths.Scripts.Manager;
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
        #endregion
        #region Unity Messages
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _operationDisplay = FindAnyObjectByType<OperationDisplay>();
            _generatorManager = FindAnyObjectByType<GeneratorManager>();
            _currentSelection = 0;
        }
        #endregion
        #region Methods
        public void SelectNumber(int button)
        {
            if (_currentSelection >= 3) return;
            int _numberSelected = _operationDisplay.GetButtonNumber(button);
            if (_numberSelected == -1) return;
            _selectedNumbers[_currentSelection] = _numberSelected;
            _operationDisplay.UpdateNumberSlot(_currentSelection + 1, _numberSelected);
            _operationDisplay.SetButtonInteractable(button, false);
            _currentSelection++;
            if(_currentSelection == 3)
            {
                /*string _playerOperation = "(" + _selectedNumbers[0] + " * " + _selectedNumbers[1] + ") + " + _selectedNumbers[2];
                bool _isCorrect = _generatorManager.ValidatePlayerOperation(_playerOperation);*/
                bool _isCorrect = _generatorManager.ValidatePlayerOperation(_selectedNumbers);
                if (_isCorrect)
                {
                    Debug.Log("Correct Operation!!\n");
                    if (_generatorManager.GenerateAndValidateExpression())
                    {
                        ResetSelection();
                        _operationDisplay.RefreshRound();
                    }
                    else
                    {
                        Debug.LogError("It can't generate the next round\n");
                    }
                }
                else
                {
                    Debug.Log("Incorrect Operation\n");
                    ResetSelection();
                }
            }
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