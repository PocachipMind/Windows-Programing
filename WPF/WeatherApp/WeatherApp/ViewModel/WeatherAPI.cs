using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.ViewModel
{
    public class WeatherAPI
    {
        // 콘스트 변수는 대문자로 작성이 기본 규칙
        public const string API_KEY = "76b70d7f9d8d4cd0e096cd9dac1cdf3b";
        public const string BASE_URL = "https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}";

        public static WeatherInformation GetWeatherInformation(string cityName)
        {
            WeatherInformation result = new WeatherInformation();

            string url = string.Format(BASE_URL, cityName, API_KEY);

            // using 문 괄호 안에 객체 하나 설정하면 
            using(HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(url);
                string json = response.Result.Content.ReadAsStringAsync().Result;

                result = JsonConvert.DeserializeObject<WeatherInformation>(json);
            }

            return result;

        }
    }
}
