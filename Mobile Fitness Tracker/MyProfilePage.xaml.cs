using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using SQLite;


namespace Mobile_Fitness_Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyProfilePage : ContentPage
    {

        
        public MyProfilePage()
        {
            InitializeComponent();
            OnAppearing();
            

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //Populate collectionview1 with user information from database
            collectionView1.ItemsSource = await App.Database.GetPeopleAsync();
                        
        }

        private void BtnClose_Clicked(object sender, EventArgs e)
        {
            //Return back to MainPage
          //  Application.Current.MainPage.Navigation.PopAsync();
            //Navigate to MainPage
            Navigation.PushAsync(new MainPage());

        }
                        

        private void BtnCreateProfile_Clicked(object sender, EventArgs e)
        {
          
            //Navigate to CreateUserPage
            Navigation.PushAsync(new CreateUserPage());

        }
    }
}