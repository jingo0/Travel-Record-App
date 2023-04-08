using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    public partial class App : Application
    {
        public static string DataBaseLocation = string.Empty;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public App(string dataBaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            DataBaseLocation = dataBaseLocation;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
