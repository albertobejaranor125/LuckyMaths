using UnityEngine;
using System.Collections.Generic;

namespace LuckyMaths.Scripts.Generator
{
    public class NumberGenerator : MonoBehaviour
    {
        #region Variables
        private List<int> _luckyNumbersGeneratedRandom;
        private List<int> _numbersSolutionGeneratedRandom;
        private List<int> _numbersDistractorGeneratedRandom;
        private int _luckyNumberRandom;
        private int _validCombinations;
        #endregion
        #region Unity Messages
        void Awake()
        {
            _luckyNumbersGeneratedRandom = new List<int>();
            _numbersSolutionGeneratedRandom = new List<int>();
            _numbersDistractorGeneratedRandom = new List<int>();
        }
        #endregion
        #region Methods
        public int GenerateLuckyNumber()
        {
            bool _numberGeneratedAndNoContainsList = false;
            if (_luckyNumbersGeneratedRandom.Count == 96)
            {
                return -1;
            }
            while (!_numberGeneratedAndNoContainsList)
            {
                _luckyNumberRandom = Random.Range(5, 101);
                if (!_luckyNumbersGeneratedRandom.Contains(_luckyNumberRandom))
                {
                    _luckyNumbersGeneratedRandom.Add(_luckyNumberRandom);
                    _numberGeneratedAndNoContainsList = true;
                }
            }
            return _luckyNumberRandom;
        }
        public int GetLuckyNumberRandom()
        {
            return _luckyNumberRandom;
        }
        public List<int> GenerateRandomNumbersSolution(OperationType type)
        {
            _numbersSolutionGeneratedRandom.Clear();
            switch (type)
            {
                case OperationType.MultiplyAdd:
                    MultiplyAddGenerate();
                    break;
                case OperationType.AddMultiply:
                    AddMultiplyGenerate();
                    break;
                case OperationType.MultiplySubtract:
                    MultiplySubtractGenerate();
                    break;
                case OperationType.AddDivide:
                    AddDivideGenerate();
                    break;
                case OperationType.MultiplyDivide:
                    MultiplyDivideGenerate();
                    break;
                default:
                    Debug.LogError("Unknown operation type\n");
                    break;
            }
            Debug.Log("Valid combinations: " + _validCombinations);
            Debug.Log("Selected combination: "+ string.Join(", ", _numbersSolutionGeneratedRandom));
            return _numbersSolutionGeneratedRandom;
        }
        public void MultiplyAddGenerate()
        {
            _validCombinations = 0;
            for (int i = 1; i <= 100; i++)
            {
                for (int j = i + 1; j <= 100; j++)
                {
                    int _product = i * j;
                    if (_product >= _luckyNumberRandom)
                    {
                        continue;
                    }
                    for (int k = 1; k < _luckyNumberRandom; k++)
                    {
                        if (k == i || k == j)
                        {
                            continue;
                        }
                        if (_product + k == _luckyNumberRandom)
                        {
                            _validCombinations++;
                            if (Random.Range(0, _validCombinations) == 0)
                            {
                                _numbersSolutionGeneratedRandom.Clear();
                                _numbersSolutionGeneratedRandom.Add(i);
                                _numbersSolutionGeneratedRandom.Add(j);
                                _numbersSolutionGeneratedRandom.Add(k);
                            }
                        }
                    }
                }
            }
        }
        public void AddMultiplyGenerate()
        {
            _validCombinations = 0;
            for (int i = 1; i <= 100; i++)
            {
                for (int j = i + 1; j <= 100; j++)
                {
                    int _sum = i + j;
                    if (_sum > _luckyNumberRandom)
                    {
                        continue;
                    }
                    for (int k = 1; k < _luckyNumberRandom; k++)
                    {
                        if (k == i || k == j)
                        {
                            continue;
                        }
                        if (_sum * k == _luckyNumberRandom)
                        {
                            _validCombinations++;
                            if (Random.Range(0, _validCombinations) == 0)
                            {
                                _numbersSolutionGeneratedRandom.Clear();
                                _numbersSolutionGeneratedRandom.Add(i);
                                _numbersSolutionGeneratedRandom.Add(j);
                                _numbersSolutionGeneratedRandom.Add(k);
                            }
                        }
                    }
                }
            }
        }
        public void MultiplySubtractGenerate()
        {
            _validCombinations = 0;
            for (int i = 1; i <= 100; i++)
            {
                for (int j = i + 1; j <= 100; j++)
                {
                    int _product = i * j;
                    if (_product <= _luckyNumberRandom)
                    {
                        continue;
                    }
                    for (int k = 1; k <= 100; k++)
                    {
                        if (k == i || k == j || k == _luckyNumberRandom)
                        {
                            continue;
                        }
                        if (_product - k == _luckyNumberRandom)
                        {
                            _validCombinations++;
                            if (Random.Range(0, _validCombinations) == 0)
                            {
                                _numbersSolutionGeneratedRandom.Clear();
                                _numbersSolutionGeneratedRandom.Add(i);
                                _numbersSolutionGeneratedRandom.Add(j);
                                _numbersSolutionGeneratedRandom.Add(k);
                            }
                        }
                    }
                }
            }
        }
        public void AddDivideGenerate()
        {
            _validCombinations = 0;
            for (int i = 1; i <= 100; i++)
            {
                for (int j = i + 1; j <= 100; j++)
                {
                    int _sum = i + j;
                    if (_sum % _luckyNumberRandom != 0)
                    {
                        continue;
                    }
                    int k = _sum / _luckyNumberRandom;
                    if (k == i || k == j || k < 1 || k > 100 || k == _luckyNumberRandom)
                    {
                        continue;
                    }
                    _validCombinations++;
                    if (Random.Range(0, _validCombinations) == 0)
                    {
                        _numbersSolutionGeneratedRandom.Clear();
                        _numbersSolutionGeneratedRandom.Add(i);
                        _numbersSolutionGeneratedRandom.Add(j);
                        _numbersSolutionGeneratedRandom.Add(k);
                    }
                }
            }
        }
        public void MultiplyDivideGenerate()
        {
            _validCombinations = 0;
            for (int i = 1; i <= 100; i++)
            {
                for (int j = i + 1; j <= 100; j++)
                {
                    int _product = i * j;
                    if (_product % _luckyNumberRandom != 0)
                    {
                        continue;
                    }
                    int k = _product / _luckyNumberRandom;
                    if (k == i || k == j || k < 1 || k > 100 || k == _luckyNumberRandom)
                    {
                        continue;
                    }
                    _validCombinations++;
                    if (Random.Range(0, _validCombinations) == 0)
                    {
                        _numbersSolutionGeneratedRandom.Clear();
                        _numbersSolutionGeneratedRandom.Add(i);
                        _numbersSolutionGeneratedRandom.Add(j);
                        _numbersSolutionGeneratedRandom.Add(k);
                    }
                }
            }
        }
        public List<int> GenerateRandomDistractorNumbers()
        {
            _numbersDistractorGeneratedRandom.Clear();
            bool _allDistractorsGenerated = false;
            while (!_allDistractorsGenerated)
            {
                int _randomNumber = Random.Range(1, 101);
                if (_randomNumber != _luckyNumberRandom &&
                    !_numbersDistractorGeneratedRandom.Contains(_randomNumber) && 
                    !_numbersSolutionGeneratedRandom.Contains(_randomNumber))
                {
                    _numbersDistractorGeneratedRandom.Add(_randomNumber);
                    
                }
                if(_numbersDistractorGeneratedRandom.Count == 3)
                {
                    _allDistractorsGenerated = true;
                }
            }
            return _numbersDistractorGeneratedRandom;
        }
        public List<int> GenerateAvailableNumbers()
        {
            List<int> _availableNumbers = new List<int>(_numbersSolutionGeneratedRandom);
            _availableNumbers.AddRange(_numbersDistractorGeneratedRandom);
            for(int i = _availableNumbers.Count -1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                int temp = _availableNumbers[i];
                _availableNumbers[i] = _availableNumbers[j];
                _availableNumbers[j] = temp;
            }
            return _availableNumbers;
        }
        #endregion
    }
}