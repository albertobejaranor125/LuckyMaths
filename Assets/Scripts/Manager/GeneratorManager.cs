using LuckyMaths.Scripts.Generator;
using LuckyMaths.Scripts.Validator;
using System.Collections.Generic;
using UnityEngine;

namespace LuckyMaths.Scripts.Manager
{
    [DefaultExecutionOrder(-100)]
    public class GeneratorManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] private int _maxAttempts = 96;
        private int _currentAttempts;
        private OperationGenerator _operationGenerator;
        private SolutionValidator _solutionValidator;
        private NumberGenerator _numberGenerator;
        private int _luckyNumberGenerated;
        private bool _expressionFounded;
        private string _expressionOperation;
        private List<int> _availableNumbers;
        #endregion
        #region Unity Messages
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _operationGenerator = FindAnyObjectByType<OperationGenerator>();
            _solutionValidator = FindAnyObjectByType<SolutionValidator>();
            _numberGenerator = FindAnyObjectByType<NumberGenerator>();
            GenerateAndValidateExpression();
        }
        #endregion
        #region Methods
        public bool GenerateAndValidateExpression()
        {
            _currentAttempts = 0;
            _expressionFounded = false;
            _luckyNumberGenerated = _numberGenerator.GenerateLuckyNumber();
            if(_luckyNumberGenerated == -1)
            {
                Debug.LogError("All lucky numbers have been generated.");
                return false;
            }
            while (_currentAttempts < _maxAttempts && !_expressionFounded)
            {
                _expressionOperation = _operationGenerator.GenerateOperation();
                if (_solutionValidator.ValidateOperation(_luckyNumberGenerated, _expressionOperation))
                {
                    Debug.Log("Operation Founded!\n");
                    _expressionFounded = true;
                }
                else
                {
                    _currentAttempts++;
                }
            }
            if (!_expressionFounded)
            {
                Debug.LogError("It hasn't been found a solution.\n");
                return false;
            }
            _numberGenerator.GenerateRandomDistractorNumbers();
            _availableNumbers = _numberGenerator.GenerateAvailableNumbers();
            return true;
        }
        public int GetLuckyNumber()
        {
            return _numberGenerator.GetLuckyNumberRandom();
        }
        public List<int> GetAvailableNumbers()
        {
            return new List<int>(_availableNumbers);
        }
        public bool ValidatePlayerOperation(string operation)
        {
            return _solutionValidator.ValidateOperation(_luckyNumberGenerated, operation);
        }
        #endregion
    }
}