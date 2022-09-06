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
        }

        private void BtnMyProfile_Clicked(object sender, EventArgs e)
        {
            //Navigate to Profile page
            Navigation.PushAsync(new MyProfilePage());


        }

        private void BtnExercise_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnClose_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}
