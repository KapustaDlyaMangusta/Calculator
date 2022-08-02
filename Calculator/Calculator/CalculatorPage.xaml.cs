using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
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

        public ICommand LongTappedEffectCommand => new Command(() =>
        {
            var question = DisplayAlert("Copy?", "Do you want to paste value", "Yes", "No");

         });
        
        private void ValidateValue()
        {
            try
            {
                Convert.ToDecimal(CalculationValue.Text);
            }
            catch
            {
                CalculationValue.Text = CalculationValue.Text.Remove(CalculationValue.Text.Length - 1, 1);
            }
        }

        private void Typing(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (CalculationValue.Text == "0" || isClicked)
            {
                if(button.Text == ".")
                {
                    CalculationValue.Text += button.Text;
                }
                else
                {

                    CalculationValue.Text = button.Text;
                    isClicked = false;
                }
            }
            else
            {
                CalculationValue.Text += button.Text;
                ValidateValue();
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

