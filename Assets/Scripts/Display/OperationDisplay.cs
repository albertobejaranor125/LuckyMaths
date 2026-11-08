using LuckyMaths.Scripts.Generator;
using LuckyMaths.Scripts.Manager;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LuckyMaths.Scripts.Display
{
    public class OperationDisplay : MonoBehaviour
    {
        #region Variables
        private GeneratorManager _generatorManager;
        [SerializeField] private TextMeshProUGUI _firstNumberSlot;
        [SerializeField] private TextMeshProUGUI _secondNumberSlot;
        [SerializeField] private TextMeshProUGUI _thirdNumberSlot;
        [SerializeField] private TextMeshProUGUI _targetNumber;
        [SerializeField] private Button _button1;
        [SerializeField] private Button _button2;
        [SerializeField] private Button _button3;
        [SerializeField] private Button _button4;
        [SerializeField] private Button _button5;
        [SerializeField] private Button _button6;
        [SerializeField] private TextMeshProUGUI _firstOperator;
        [SerializeField] private TextMeshProUGUI _secondOperator;
        #endregion
        #region Unity Messages
        void Start()
        {
            _generatorManager = FindAnyObjectByType<GeneratorManager>();
            ClearNumberSlots();
            _targetNumber.text = _generatorManager.GetLuckyNumber().ToString();
            DisplayAvailableNumbers(_generatorManager.GetAvailableNumbers());
            DisplayOperationType();
        }
        #endregion
        #region Methods
        public void UpdateNumberSlot(int slot, int number)
        {
            if(slot == 1)
            {
                _firstNumberSlot.text = number.ToString();
            }else if(slot == 2)
            {
                _secondNumberSlot.text = number.ToString();
            }else if(slot == 3)
            {
                _thirdNumberSlot.text = number.ToString();
            }
            else
            {
                Debug.LogWarning("Invalid number slot: " + slot);
            }
        }
        public void ClearNumberSlots()
        {
            _firstNumberSlot.text = "_";
            _secondNumberSlot.text = "_";
            _thirdNumberSlot.text = "_";
        }
        public void SetButtonInteractable(int button, bool interactable)
        {
            switch (button)
            {
                case 0: _button1.interactable = interactable; break;
                case 1: _button2.interactable = interactable; break;
                case 2: _button3.interactable = interactable; break;
                case 3: _button4.interactable = interactable; break;
                case 4: _button5.interactable = interactable; break;
                case 5: _button6.interactable = interactable; break;
                default:
                    Debug.LogWarning("Invalid button: " + button);
                    break;
            }
        }
        public int GetButtonNumber(int button)
        {
            switch (button)
            {
                case 0: return int.Parse(_button1.GetComponentInChildren<TextMeshProUGUI>().text);
                case 1: return int.Parse(_button2.GetComponentInChildren<TextMeshProUGUI>().text);
                case 2: return int.Parse(_button3.GetComponentInChildren<TextMeshProUGUI>().text);
                case 3: return int.Parse(_button4.GetComponentInChildren<TextMeshProUGUI>().text);
                case 4: return int.Parse(_button5.GetComponentInChildren<TextMeshProUGUI>().text);
                case 5: return int.Parse(_button6.GetComponentInChildren<TextMeshProUGUI>().text);
                default:
                    Debug.LogWarning("Invalid button: " + button);
                    return -1;
            }
        }
        public void DisplayAvailableNumbers(List<int> numbers)
        {
            if(numbers == null || numbers.Count != 6)
            {
                Debug.LogError("Invalid number list\n");
                return;
            }
            _button1.GetComponentInChildren<TextMeshProUGUI>().text = numbers[0].ToString();
            _button2.GetComponentInChildren<TextMeshProUGUI>().text = numbers[1].ToString();
            _button3.GetComponentInChildren<TextMeshProUGUI>().text = numbers[2].ToString();
            _button4.GetComponentInChildren<TextMeshProUGUI>().text = numbers[3].ToString();
            _button5.GetComponentInChildren<TextMeshProUGUI>().text = numbers[4].ToString();
            _button6.GetComponentInChildren<TextMeshProUGUI>().text = numbers[5].ToString();
        }
        public void RefreshRound()
        {
            _targetNumber.text = _generatorManager.GetLuckyNumber().ToString();
            DisplayAvailableNumbers(_generatorManager.GetAvailableNumbers());
            DisplayOperationType();
        }
        private void DisplayOperationType()
        {
            switch (_generatorManager.GetCurrentOperationType())
            {
                case OperationType.MultiplyAdd:
                    _firstOperator.text = "*";
                    _secondOperator.text = "+";
                    break;
                case OperationType.AddMultiply:
                    _firstOperator.text = "+";
                    _secondOperator.text = "*";
                    break;
                case OperationType.MultiplySubtract:
                    _firstOperator.text = "*";
                    _secondOperator.text = "-";
                    break;
                case OperationType.AddDivide:
                    _firstOperator.text = "+";
                    _secondOperator.text = "/";
                    break;
                case OperationType.MultiplyDivide:
                    _firstOperator.text = "*";
                    _secondOperator.text = "/";
                    break;
                default:
                    Debug.LogError("Unknown operation type\n");
                    break;
            }
        }
        public void SetSelectedNumbersColor(Color color)
        {
            _firstNumberSlot.color = color;
            _secondNumberSlot.color = color;
            _thirdNumberSlot.color = color;
        }
        public void ResetSelectedNumbersColor()
        {
            SetSelectedNumbersColor(Color.white);
        }
        #endregion
    }
}