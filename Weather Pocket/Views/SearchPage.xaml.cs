using System;
using Newtonsoft.Json;
using Weather_Pocket.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather_Pocket.Helper;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Globalization;

namespace Weather_Pocket.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        CultureInfo idID = new CultureInfo("id-ID");
        public SearchPage()
        {
            InitializeComponent();
        }
        private void OnSearchButtonPressed(object sender, EventArgs e)
        {
            // Get the search term and clear the search bar
            var searchTerm = searchBar.Text;
            searchBar.Text = string.Empty;

            // Update the label with the search term
            GetWeatherInfo(searchTerm);
            //SearchTerm.Text = searchTerm;
            //Debug.WriteLine(searchTerm);
            // TODO: Perform the search
        }

        private async void GetWeatherInfo(string query)
        {
            string url = $"http://api.openweathermap.org/data/2.5/forecast?q={query}&appid=ff64311a5e75d1a3dc662c4fb7bb28a5&units=metric&lang=id";
            var result = await ApiCaller.Get(url);
            
            if(result.Successful)
            {
                try
                {
                    var forecast = JsonConvert.DeserializeObject<Forecast>(result.Response);
                    cityText.Text = forecast.city.name;
                    //.Text = char.ToUpper(forecast.list[0].weather[0].description[0]) + forecast.list[0].weather[0].description.Substring(1);
                    labelText1.Text = "Hari ini";
                    degreText1.Text = "°";
                    imgIcon1.Source = $"w{forecast.list[0].weather[0].icon}";
                    tempratureTxt1.Text = forecast.list[0].main.temp.ToString("0");labelText1.Text = "Hari ini";

                    labelText2.Text = "Besok";
                    degreText2.Text = "°";
                    imgIcon2.Source = $"w{forecast.list[6].weather[0].icon}";
                    tempratureTxt2.Text = forecast.list[6].main.temp.ToString("0");

                    labelText3.Text = DateTime.Parse(forecast.list[14].dt_txt).ToLocalTime().ToString("dddd", idID);
                    degreText3.Text = "°";
                    imgIcon3.Source = $"w{forecast.list[14].weather[0].icon}";
                    tempratureTxt3.Text = forecast.list[14].main.temp.ToString("0"); 
                    
                    labelText4.Text = DateTime.Parse(forecast.list[20].dt_txt).ToLocalTime().ToString("dddd", idID);
                    degreText4.Text = "°";
                    imgIcon4.Source = $"w{forecast.list[20].weather[0].icon}";
                    tempratureTxt4.Text = forecast.list[20].main.temp.ToString("0");
                    

                }
                catch (Exception e)
                {
                    await DisplayAlert("Weather Info", e.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Weather Info", "Location Not Found!", "OK");
            }
        }

    }
}