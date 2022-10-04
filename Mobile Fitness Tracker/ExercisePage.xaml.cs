using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Mobile_Fitness_Tracker.Database;

namespace Mobile_Fitness_Tracker
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExercisePage : ContentPage
    {
        public ExercisePage()
        {
            InitializeComponent();
           
         
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //Output profile picture
            ImgExercise.Source = UserGlobalVaraibles.ProfilePic;
            //Output Name to label
            LblNickName.Text = UserGlobalVaraibles.FirstName;
            

        }

        private void BtnExerciseList_Clicked(object sender, EventArgs e)
        {
            //Navigate to Exercise List Page
            Navigation.PushAsync(new ExerciseListPage());
        }

        private void BtnWorkoutList_Clicked(object sender, EventArgs e)
        {
            //Navigate to Workout List Page
            Navigation.PushAsync(new WorkoutListPage());
        }

        private void BtnWorkoutSchedule_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnClose_Clicked(object sender, EventArgs e)
        {

        }
    }
}