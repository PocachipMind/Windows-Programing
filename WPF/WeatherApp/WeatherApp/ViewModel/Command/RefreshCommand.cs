using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WeatherApp.ViewModel.Command
{
    public class RefreshCommand : ICommand
    {
        public WeatherVM VM { get; set; }

        public event EventHandler CanExecuteChanged;

        public RefreshCommand(WeatherVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            string Text = parameter.ToString();
            if(Text == "")
            {
                VM.GetWeather();
            }
            else
            {
                if(VM.Cities.Contains(Text)) //같은 이름의 도시 있음.
                {
                    VM.SelectedCity = Text;
                }
                else
                {
                    VM.Cities.Add(Text);
                    VM.SelectedCity = Text;
                }
                
            }
            
        }
    }
}
