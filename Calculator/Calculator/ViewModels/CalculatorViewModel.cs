using Calculator.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Calculator.ViewModels
{
    public class CalculatorViewModel : BaseViewModel
    {
        private decimal _firstNumber;
        private string _operatorName;
        private bool _isClicked;
        private string _calculateValue = Constants.Zero;

        public CalculatorViewModel()
        {
            AbsoluteClearCommand = new Command(AbsoluteClear);
            TypingCommand = new Command<string>(Typing);
            ClearCommand = new Command(Clear);
            OperationCommand = new Command<string>(Operation);
            PercentageCommand = new Command(Percentage);
            EqualCommand = new Command(Equal);
        }


        public string CalculateValue
        {
            get => _calculateValue;
            set
            {
                var temp = _calculateValue;
                _calculateValue = ValidateValue.Validate(value) ? value : temp;
                SetProperty(nameof(CalculateValue));
            }
        }

        public ICommand TypingCommand { get; }
        
        public ICommand AbsoluteClearCommand { get; }
        
        public ICommand ClearCommand { get; } 

        public ICommand OperationCommand { get; } 

        public ICommand PercentageCommand { get; }  

        public ICommand EqualCommand { get; } 

        private decimal Calculate(decimal firstNumber, decimal secondNumber) => _operatorName switch
        {
            Constants.Plus => firstNumber + secondNumber,
            Constants.Minus => firstNumber - secondNumber,
            Constants.Multiply => firstNumber * secondNumber,
            Constants.Divide => firstNumber / secondNumber,
            _ => 0
        };

        private void AbsoluteClear()
        {
            CalculateValue = Constants.Zero;
            _isClicked = false;
            _firstNumber = 0;
        }

        private void Typing(string value)
            {

            if (CalculateValue == Constants.Zero || _isClicked)
            {
                if (value == Constants.Dot)
                {
                    CalculateValue += value;
                }
                else
                {

                    CalculateValue = value;
                    _isClicked = false;
                }
            }
            else
            {
                CalculateValue += value;

            };
        }

        private void Clear()
        {
            string value = CalculateValue;
            if (value != Constants.Zero)
            {
                value = value.Remove(value.Length - 1, 1);
                if (string.IsNullOrEmpty(value))
                {
                    CalculateValue = Constants.Zero;
                }
                else
                {
                    CalculateValue = value;
                }
            }
        }

        private void Operation(string value)
        {
                _isClicked = true;
                _operatorName = value;
                _firstNumber = Convert.ToDecimal(_calculateValue);
        }

        private void Percentage()
        {
            try
            {
                string value = _calculateValue;
                if (value != Constants.Zero)
                {
                    decimal percentValue = Convert.ToDecimal(value);
                    string result = (percentValue / 100).ToString(Constants.FloatValueMaxLenght);
                    CalculateValue = result;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("You did smth bad", ex.Message, "Ok");
            }
        }

        private void Equal()
        {
            try
            {
                decimal secondNumber = Convert.ToDecimal(_calculateValue);
                string finalResult = Calculate(_firstNumber, secondNumber).ToString(Constants.FloatValueMaxLenght);
                CalculateValue = finalResult;
            }
            catch (Exception ex)
            {
                DisplayAlert("You did smth bad", ex.Message, "Ok");
            }
        }
    }
}
