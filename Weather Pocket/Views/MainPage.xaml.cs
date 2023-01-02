using Weather_Pocket.Models;
using Weather_Pocket.Helper;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;

namespace Weather_Pocket.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            GetLocation();
            if (VersionTracking.IsFirstLaunchEver)
            {
                Navigation.PushModalAsync(new OnboardingPage());
            }
        }
        private string Location = "Yogyakarta";

        private double Latitude { get; set; }
        private double Longitude { get; set; }


        private async void GetLocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }
                if (location == null)
                    await DisplayAlert("Weather Info", "No GPS", "OK");
                else
                    Longitude = location.Longitude;
                    Latitude = location.Latitude;


                GetWeatherInfo();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Weather Info", ex.Message, "OK");
            }
        }

        private async void GetWeatherInfo()
        {
            var url = $"http://api.openweathermap.org/data/2.5/weather?lat={Latitude}&lon={Longitude}&appid=ff64311a5e75d1a3dc662c4fb7bb28a5&units=metric&lang=id";

            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                try
                {

                    var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(result.Response);
                    //descriptionTxt.Text = weatherInfo.weather[0].description;
                    descriptionTxt.Text = char.ToUpper(weatherInfo.weather[0].description[0]) + weatherInfo.weather[0].description.Substring(1);
                    iconImg.Source = $"w{weatherInfo.weather[0].icon}";
                    cityTxt.Text = weatherInfo.name;
                    temperatureTxt.Text = weatherInfo.main.temp.ToString("0");
                    humidityTxt.Text = $"{weatherInfo.main.humidity}%";
                    pressureTxt.Text = $"{weatherInfo.main.pressure} hpa";
                    windTxt.Text = $"{weatherInfo.wind.speed} m/s";
                    cloudinessTxt.Text = $"{weatherInfo.clouds.all}%";

                    var dt = new DateTime().ToUniversalTime().AddSeconds(weatherInfo.dt);
                    dateTxt.Text = dateTxt.Text = DateTime.Now.ToString("dddd, dd MMMM").ToUpper();

                    //GetForecast();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Weather Info", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Weather Info", "No weather information found", "OK");
            }
        }
    }
}