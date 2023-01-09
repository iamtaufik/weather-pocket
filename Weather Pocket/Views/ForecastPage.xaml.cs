using System;
using Newtonsoft.Json;
using Weather_Pocket.Helper;
using Weather_Pocket.Models;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Weather_Pocket.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForecastPage : ContentPage
    {
        public ForecastPage()
        {
            InitializeComponent();
            GetLocation();
        }
        
        private void BackPage(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }

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
            var url = $"http://api.openweathermap.org/data/2.5/forecast?lat={Latitude}&lon={Longitude}&appid=ff64311a5e75d1a3dc662c4fb7bb28a5&units=metric&lang=id";

            var result = await ApiCaller.Get(url);

            if (result.Successful)
            {
                try
                {

                    var forecast = JsonConvert.DeserializeObject<Forecast>(result.Response);
                    
                    timeTxt1.Text = DateTime.Parse(forecast.list[0].dt_txt).ToLocalTime().ToString("HH:mm");
                    iconImg1.Source = $"w{forecast.list[0].weather[0].icon}";
                    descriptionTxt1.Text = forecast.list[0].weather[0].description;

                    timeTxt2.Text = DateTime.Parse(forecast.list[1].dt_txt).ToLocalTime().ToString("HH:mm");
                    iconImg2.Source = $"w{forecast.list[1].weather[0].icon}";
                    descriptionTxt2.Text = forecast.list[1].weather[0].description;
                    
                    timeTxt3.Text = DateTime.Parse(forecast.list[2].dt_txt).ToLocalTime().ToString("HH:mm");
                    iconImg3.Source = $"w{forecast.list[2].weather[0].icon}";
                    descriptionTxt3.Text = forecast.list[2].weather[0].description;
                    
                    timeTxt4.Text = DateTime.Parse(forecast.list[3].dt_txt).ToLocalTime().ToString("HH:mm");
                    iconImg4.Source = $"w{forecast.list[3].weather[0].icon}";
                    descriptionTxt4.Text = forecast.list[3].weather[0].description;
                    
                    timeTxt5.Text = DateTime.Parse(forecast.list[4].dt_txt).ToLocalTime().ToString("HH:mm");
                    iconImg5.Source = $"w{forecast.list[4].weather[0].icon}";
                    descriptionTxt5.Text = forecast.list[4].weather[0].description;
                    
                    timeTxt6.Text = DateTime.Parse(forecast.list[5].dt_txt).ToLocalTime().ToString("HH:mm");
                    iconImg6.Source = $"w{forecast.list[5].weather[0].icon}";
                    descriptionTxt6.Text = forecast.list[5].weather[0].description;
                    
                    timeTxt7.Text = DateTime.Parse(forecast.list[6].dt_txt).ToLocalTime().ToString("HH:mm");
                    iconImg7.Source = $"w{forecast.list[6].weather[0].icon}";
                    descriptionTxt7.Text = forecast.list[6].weather[0].description;
                    
                    timeTxt8.Text = DateTime.Parse(forecast.list[7].dt_txt).ToLocalTime().ToString("HH:mm");
                    iconImg8.Source = $"w{forecast.list[7].weather[0].icon}";
                    descriptionTxt8.Text = forecast.list[7].weather[0].description;



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