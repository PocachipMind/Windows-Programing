using Calculator.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    public enum SelectedOperator //열거형
    {
        Addition, Substraction, Multiplication, Division
    }

    public partial class MainWindow : Window
    {
        string calculation;  
        double lastValue;
        double funtionValue;
        bool isFirstCompute = true;
        SelectedOperator selectedOperator;
        List<string> history = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        // 연산 버튼 클릭 ( +, -, *, / )
        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            double oldValue = lastValue;

            if (double.TryParse(resultLabel.Content.ToString(), out lastValue))
            {
                resultLabel.Content = "0";
                calculation += $"{lastValue} ";
            }

            if (!isFirstCompute) // 첫 계산일 경우 그냥 lastValue에 resultLabel.Content 저장
            {
                switch (selectedOperator) // 첫 계산 아닐경우 lastValue에 계산해서 갱신
                {
                    case SelectedOperator.Addition:
                        lastValue = SimpleMath.Add(oldValue, lastValue);
                        break;
                    case SelectedOperator.Substraction:
                        lastValue = SimpleMath.Sub(oldValue, lastValue);
                        break;
                    case SelectedOperator.Multiplication:
                        lastValue = SimpleMath.Mul(oldValue, lastValue);
                        break;
                    case SelectedOperator.Division:
                        lastValue = SimpleMath.Div(oldValue, lastValue);
                        break;
                }
            }
            else
            {
                isFirstCompute = false;
            }

            if (sender == mulButton)
            {
                selectedOperator = SelectedOperator.Multiplication;
                calculation += "* ";
                calculationTextBlock.Text = calculation;
            }
            if (sender == divButton)
            {
                selectedOperator = SelectedOperator.Division;
                calculation += "/ ";
                calculationTextBlock.Text = calculation;
            }
            if (sender == subButton)
            {
                selectedOperator = SelectedOperator.Substraction;
                calculation += "- ";
                calculationTextBlock.Text = calculation;
            }
            if (sender == addButton)
            {
                selectedOperator = SelectedOperator.Addition;
                calculation += "+ ";
                calculationTextBlock.Text = calculation;
            }

        }

        // 숫자 버튼 클릭 ( 0 ~ 9 )
        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = 0;

            // var button = sender as Button; // var button = (System.Windows.Controls.Button)sender;
            // selectedValue = int.Parse(button.Content.ToString());

            if (sender == zeroButton)
            {
                selectedValue = 0;
            }
            if (sender == oneButton)
            {
                selectedValue = 1;
            }
            if (sender == twoButton)
            {
                selectedValue = 2;
            }
            if (sender == threeButton)
            {
                selectedValue = 3;
            }
            if (sender == fourButton)
            {
                selectedValue = 4;
            }
            if (sender == fiveButton)
            {
                selectedValue = 5;
            }
            if (sender == sixButton)
            {
                selectedValue = 6;
            }
            if (sender == sevenButton)
            {
                selectedValue = 7;
            }
            if (sender == eightButton)
            {
                selectedValue = 8;
            }
            if (sender == nineButton)
            {
                selectedValue = 9;
            }


            if(resultLabel.Content.ToString() == "0")
            {
                resultLabel.Content = $"{selectedValue}";
            }
            else 
            {
                resultLabel.Content = $"{resultLabel.Content}{selectedValue}";
            }
        }

        // equal 버튼 클릭 ( = )
        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            //연산 수행
            double newNumber = 0;
            double result = 0;
            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = SimpleMath.Add(lastValue, newNumber);
                        break;
                    case SelectedOperator.Substraction:
                        result = SimpleMath.Sub(lastValue, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.Mul(lastValue, newNumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMath.Div(lastValue, newNumber);
                        break;
                }
            }

            resultLabel.Content = result.ToString();

            calculation += $"{newNumber} = {result}";
            calculationTextBlock.Text = "";
            history.Add(calculation);
            historyLstView.ItemsSource = history;
            historyLstView.Items.Refresh();
            calculation = "";
            // 변수들 초기값으로 되돌려놓기 ( = 버튼 여러번 누를 시 예시 프로그램과 동일하게 동작하기 위함 )
            lastValue = 0;
            selectedOperator = SelectedOperator.Addition; 
            isFirstCompute = true;
        }

        // 점 버튼 클릭 ( . )
        private void DotButton_Click(object sender, RoutedEventArgs e)
        {
            if (!resultLabel.Content.ToString().Contains("."))
            {
                resultLabel.Content = resultLabel.Content.ToString() + ".";
            }
        }

        // 기능 버튼 클릭 ( Ac, +/- , %, Del, Sqrt, x^2, 1/x )
        // AC 버튼
        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
        }

        // +/- 버튼
        private void NegativeButton_Click(object sender, RoutedEventArgs e)
        {
            if(double.TryParse(resultLabel.Content.ToString(),out funtionValue))
            {
                funtionValue *= -1;
                resultLabel.Content = funtionValue.ToString();
            }

        }

        // % 버튼
        private void PercentButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out funtionValue))
            {
                funtionValue /= 100;
                resultLabel.Content = funtionValue.ToString();
            }

        }

        // Del 버튼
        private void delButton_Click(object sender, RoutedEventArgs e)
        {
            if( resultLabel.Content.ToString().Length == 1 )
            {
                resultLabel.Content = "0";
            }
            else
            {
                resultLabel.Content = resultLabel.Content.ToString().Substring(0, resultLabel.Content.ToString().Length - 1);
            }

        }

        // Sqrt 버튼
        private void sqrtButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out funtionValue))
            {
                if(funtionValue > 0)
                {
                    funtionValue = Math.Sqrt(funtionValue);
                    resultLabel.Content = funtionValue.ToString();
                }
            }
            
        }

        // x^2 버튼
        private void powerButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out funtionValue))
            {
                funtionValue *= funtionValue;
                resultLabel.Content = funtionValue.ToString();
            }
        }

        // 1/x 버튼
        private void denominatorButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out funtionValue))
            {
                funtionValue = 1 / funtionValue;
                resultLabel.Content = funtionValue.ToString();
            }
        }


    }
}
