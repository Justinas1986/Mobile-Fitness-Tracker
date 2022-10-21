using Plugin.Media;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;

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
           
            //initialize DataComponent in App Startup of App.xaml.cs 
            Xamarin.Forms.DataGrid.DataGridComponent.Init();
            // MainPage = new MainPage();
            //Allows to navigate from MainPage to MyProfilePage
            MainPage = new NavigationPage(new MainPage());
            //Synfussion datagrid licese
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzI2MTYzQDMyMzAyZTMyMmUzMGtOaVhudW9uZi9Gc3J6Z1Fwcm0zeG5Ea1VyRUtjdHZYamp4aG5xVzZQS1k9");

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
