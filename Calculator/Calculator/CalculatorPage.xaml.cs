using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Calculator
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalculatorPage : ContentPage
    {
        public CalculatorPage()
        {
            InitializeComponent();
        }

        private decimal firstNumber;
        private string operatorName;
        private bool isClicked;

        private bool CheckOnDot()
        {
            for (int i = 0; CalculationValue.Text.Length < i; i++)
            {
                if (CalculationValue.Text[i] == '.')
                {
                    return false;
                }

            }
            return true;
        }

        private void Typing(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (CalculationValue.Text == "0" || isClicked)
            {
                isClicked = false;
                if (button.Text == ".")
                {
                    if(CheckOnDot() == true)
                    {
                        CalculationValue.Text += button.Text;
                    }
                    else
                    {
                        CalculationValue.Text = CalculationValue.Text;
                    }
                }
                else
                {
                    CalculationValue.Text = button.Text;
                }
            }
            else
            {
                CalculationValue.Text += button.Text;
            }
        }

        private void AbsoluteClear(object sender, EventArgs e)
        {
            CalculationValue.Text = "0";
            isClicked = false;
            firstNumber = 0;
        }

        private void Clear(object sender, EventArgs e)
        {
            string number = CalculationValue.Text;
            if (number != "0")
            {
                number = number.Remove(number.Length - 1, 1);
                if (string.IsNullOrEmpty(number))
                {
                    CalculationValue.Text = "0";
                }
                else
                {
                    CalculationValue.Text = number;
                }
            }
        }

        private void Operation(object sender, EventArgs e)
        {
            var button = sender as Button;
            isClicked = true;
            operatorName = button.Text;
            firstNumber = Convert.ToDecimal(CalculationValue.Text);
        }

        private async void Percentage(object sender, EventArgs e)
        {
            try
            {
                string number = CalculationValue.Text;
                if (number != "0")
                {
                    decimal percentValue = Convert.ToDecimal(number);
                    string result = (percentValue / 100).ToString("0.#########");
                    CalculationValue.Text = result;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("You did smth bad", ex.Message, "Ok");
            }
        }

        private void Equal(object sender, EventArgs e)
        {
            try
            {
                decimal secondNumber = Convert.ToDecimal(CalculationValue.Text);
                string finalResult = Calculate(firstNumber, secondNumber).ToString("0.#########");
                CalculationValue.Text = finalResult;
            }
            catch (Exception ex)
            {
                DisplayAlert("You did smth bad", ex.Message, "Ok");
            }
        }

        public decimal Calculate(decimal firstNumber, decimal secondNumber)
        {
            decimal result = 0;
            switch (operatorName)
            {
                case "+":
                    result = firstNumber + secondNumber;
                    return result;
                case "-": 
                    result = firstNumber - secondNumber;
                    return result;
                case "X":
                    result = firstNumber * secondNumber;
                    return result;
                case "/": 
                    result = firstNumber / secondNumber;
                    return result;
                default:
                    return result;
            }
            
        }
    }
}

