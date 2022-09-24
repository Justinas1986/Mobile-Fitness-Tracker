using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mobile_Fitness_Tracker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            OnAppearing();

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //variable to get userprofile info
            var table = await App.Database.GetPeopleAsync();
            //loop to read from variable table
            foreach (var s in table)
            {                
                //get First Name from DB to global variable
                UserGlobalVaraibles.FirstName = s.FirstName;
                //get Profile picture from DB to global variable
                UserGlobalVaraibles.ProfilePic = s.ProfilePic;
            }
            //check if username is entered, then enable exercise button
            if (UserGlobalVaraibles.FirstName != null)
            {
                BtnExercise.IsEnabled = true;
            }


        }


        private void BtnMyProfile_Clicked(object sender, EventArgs e)
        {
            //Navigate to Profile page
            Navigation.PushAsync(new MyProfilePage());


        }

        private void BtnExercise_Clicked(object sender, EventArgs e)
        {
            //Navigate to MainPage
            Navigation.PushAsync(new ExercisePage());
        }

        private void BtnClose_Clicked(object sender, EventArgs e)
        {
            
        }


       
        

    }
}
