using System;
using Weather_Pocket.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Weather_Pocket.ViewModels
{
    class OnboardingViewModel : MvvmHelpers.BaseViewModel
    {
        private ObservableCollection<OnboardingModel> items;
        private int position;
        private string skipButtonText;

        public OnboardingViewModel()
        {
            SetSkipButtonText("Selanjutnya");
            InitializeOnBoarding();
            InitializeSkipCommand();
        }

        private void SetSkipButtonText(string skipButtonText)
                => SkipButtonText = skipButtonText;

        private void InitializeOnBoarding()
        { 
            items = new ObservableCollection<OnboardingModel>
            {
              new OnboardingModel
              {
                Title = "Memperkirakan Cuaca",
                Image = "raining.gif",
                Description = "Dengan aplikasi Weather Pocket kalian bisa melihat cuaca yang akan datang sekarang "
              },
              new OnboardingModel
              {
                Title = "Fitur Search",
                Image = "searching.gif",
                Description = "Dengan fitur ini kalian bisa mencari dikota mana yang sedang terkena hujan atau tidak dan sebaliknya sedang cerah atau tidak"
              },
            };
        }

        private void InitializeSkipCommand()
        {
            SkipCommand = new Command(() =>
            {
                if (LastPositionReached())
                {
                    ExitOnBoarding();
                }
                else
                {
                    MoveToNextPosition();
                }
            });
        }

        private static void ExitOnBoarding()
            => Application.Current.MainPage.Navigation.PopModalAsync();

        private void MoveToNextPosition()
        {
            var nextPosition = ++Position;
            Position = nextPosition;
        }

        private bool LastPositionReached()
            => Position == Items.Count - 1;

        public ObservableCollection<OnboardingModel> Items
        {
            get => items;
            set => SetProperty(ref items, value);
        }

        public string SkipButtonText
        {
            get => skipButtonText;
            set => SetProperty(ref skipButtonText, value);
        }

        public int Position
        {
            get => position;
            set
            {
                if (SetProperty(ref position, value))
                {
                    UpdateSkipButtonText();
                }
            }
        }

        private void UpdateSkipButtonText()
        {
            if (LastPositionReached())
            {
                SetSkipButtonText("Mulai");
            }
            else
            {
                SetSkipButtonText("Selanjutnya");
            }
        }

        public ICommand SkipCommand { get; private set; }
    }
}
