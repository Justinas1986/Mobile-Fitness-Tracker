using Plugin.Media;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile_Fitness_Tracker
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

           // MainPage = new MainPage();
            //Allows to navigate from MainPage to MyProfilePage
            MainPage = new NavigationPage(new MainPage());
            //CrossMedia.Current.Initialize();


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
