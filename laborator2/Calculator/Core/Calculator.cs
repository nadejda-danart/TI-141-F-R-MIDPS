using System;
using static System.Double;

namespace Core
{
    public class Calculator
    {
        private double left;
        private string operation;

        public delegate void CalculatedEventHandler(double result);
        public delegate void ErrorEventHandler();

        public event CalculatedEventHandler OnCalculate;
        public event ErrorEventHandler OnError;

        public bool CanCalculate
        {
            get { return !string.IsNullOrEmpty(operation);  }
        }

        public void Add(double number)
        {
            if (CanCalculate)
            {
                left = Calculate(number);
                FireCalculate(left);
            }
            else
            {
                left = number;
            }
        }

        public void Add(string operation)
        {
            this.operation = operation;
        }

        public void Clear()
        {
            operation = "";
        }

        private double Calculate(double right)
        {
            switch (operation)
            {
                case "+":
                    return left + right;
                case "-":
                    return left - right;
                case "×":
                    return left * right;
                case "/":
                    return left / right;
                case "xⁿ":
                    return Math.Pow(left, right);
                case "√":
                    return Math.Pow(left, 1/right);
            }
            return left;
        }

        private void FireCalculate(double result)
        {
            if (OnCalculate != null)
            {
                OnCalculate.Invoke(result);
            }
            if (OnError != null && (IsInfinity(result) || IsNaN(result)))
            {
                OnError.Invoke();
            }
        }
    }
}
