using Plugin.Media;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace Mobile_Fitness_Tracker
{
    public partial class App : Application
    {
        private static Database database;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "people.db3"));
                }

                return database;

            }
        }



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
