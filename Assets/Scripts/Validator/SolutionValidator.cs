using UnityEngine;
using System;
using NCalc;

namespace LuckyMaths.Scripts.Validator
{
    public class SolutionValidator : MonoBehaviour
    {
        #region Variables
        #endregion
        #region Unity Messages
        #endregion
        #region Methods
        public bool ValidateOperation(int luckyNumber, string operationGenerated)
        {
            Expression _expressionConversionOperation = new Expression(operationGenerated);
            decimal _resultOperation = Convert.ToDecimal(_expressionConversionOperation.Evaluate());
            if (_resultOperation % 1 != 0) return false;
            int _conversionOperation = (int) _resultOperation;
            Debug.Log("Conversion Operation: " + _conversionOperation + "\n");
            return luckyNumber == _conversionOperation;
        }
        #endregion
    }
}