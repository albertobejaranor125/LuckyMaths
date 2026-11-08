using System.Collections.Generic;
using UnityEngine;

namespace LuckyMaths.Scripts.Generator
{
    public enum OperationType
    {
        MultiplyAdd,
        AddMultiply,
        MultiplySubtract,
        AddDivide,
        MultiplyDivide
    }
    public class OperationGenerator : MonoBehaviour
    {
        #region Variables
        #endregion
        #region Unity Messages
        #endregion
        #region Methods
        public OperationType GenerateOperationType()
        {
            int _operationTypesCount = System.Enum.GetValues(typeof(OperationType)).Length;
            int _randomIndex = Random.Range(0, _operationTypesCount);
            return (OperationType) _randomIndex;
        }
        public string GenerateOperation(OperationType operationType, List<int> numbers)
        {
            if(numbers == null || numbers.Count != 3)
            {
                Debug.LogError("Invalid numbers for operation generation\n");
                return "";
            }
            int _firstNumber = numbers[0];
            int _secondNumber = numbers[1];
            int _thirdNumber = numbers[2];
            string _operationGenerated = "";
            switch (operationType)
            {
                case OperationType.MultiplyAdd:
                    _operationGenerated = $"({_firstNumber} * {_secondNumber}) + {_thirdNumber}";
                    break;
                case OperationType.AddMultiply:
                    _operationGenerated = $"({_firstNumber} + {_secondNumber}) * {_thirdNumber}";
                    break;
                case OperationType.MultiplySubtract:
                    _operationGenerated = $"({_firstNumber} * {_secondNumber}) - {_thirdNumber}";
                    break;
                case OperationType.AddDivide:
                    _operationGenerated = $"({_firstNumber} + {_secondNumber}) / {_thirdNumber}";
                    break;
                case OperationType.MultiplyDivide:
                    _operationGenerated = $"({_firstNumber} * {_secondNumber}) / {_thirdNumber}";
                    break;
                default:
                    Debug.LogError("Unknown operation type\n");
                    return "";
            }
            Debug.Log("Operation Generated: " + _operationGenerated);
            return _operationGenerated;
        }
        #endregion
    }
}