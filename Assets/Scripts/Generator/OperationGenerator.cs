using System.Collections.Generic;
using UnityEngine;

namespace LuckyMaths.Scripts.Generator
{
    public class OperationGenerator : MonoBehaviour
    {
        #region Variables
        private NumberGenerator _numberGenerator;
        private List<string> _operatorsList = new List<string>(){
                "+", "-", "*", "/"
            };
        #endregion
        #region Unity Messages
        void Awake()
        {
            _numberGenerator = FindAnyObjectByType<NumberGenerator>();
        }
        #endregion
        #region Methods
        public string GenerateOperation()
        {
            string _operationGenerated = "";
            List<int> _correctNumbersOperation = _numberGenerator.GenerateRandomNumbersSolution();
            _operationGenerated += "(" + _correctNumbersOperation[0].ToString() + " " + _operatorsList[2] + " " + _correctNumbersOperation[1].ToString() + ") " + _operatorsList[0] + " " + _correctNumbersOperation[2].ToString();
            Debug.Log("Operation Generated: " + _operationGenerated + "\n");
            return _operationGenerated;
        }
        #endregion
    }
}