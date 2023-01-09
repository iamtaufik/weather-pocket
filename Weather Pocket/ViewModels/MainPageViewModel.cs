using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Weather_Pocket.Views;
using Xamarin.Forms;

namespace Weather_Pocket.ViewModels
{
    class MainPageViewModel : ContentPage
    {
        public MainPageViewModel()
        {
            SearchCommand();
        }
        public async void SearchCommand()
        {
          await  Application.Current.MainPage.Navigation.PushAsync(new SearchPage());
        }
    }
}
