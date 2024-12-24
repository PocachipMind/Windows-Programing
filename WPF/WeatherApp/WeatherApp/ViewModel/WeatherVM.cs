using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;
using WeatherApp.ViewModel.Command;

namespace WeatherApp.ViewModel
{
    public class WeatherVM
    {
        public WeatherInformation Weather { get; set; }
        public ObservableCollection<string> Cities { get; set; }

        private string selectedCity;

        public string SelectedCity
        {
            get { return selectedCity; }
            set 
            {
                selectedCity = value;
                GetWeather();
            }
        }

        public RefreshCommand RefreshCommand { get; set; }

        public WeatherVM()
        {
            Weather = new WeatherInformation();
            Cities = new ObservableCollection<string>();

            Cities.Add("London");
            Cities.Add("Paris");
            Cities.Add("Jeonju");
            Cities.Add("Seoul");

            RefreshCommand = new RefreshCommand(this);
        }

        public void GetWeather()
        {
            if(SelectedCity != null)
            {
                var weather = WeatherAPI.GetWeatherInformation(SelectedCity); 
                Weather.Name = weather.Name;
                Weather.Main = weather.Main;
                Weather.Wind = weather.Wind;
            }
        }
    }
}
