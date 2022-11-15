using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile_Fitness_Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutStartPage : ContentPage
    {
        public WorkoutStartPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();           
            //get exercise from db related for particular workout
            var table = await App.Database.WorkoutExerciseAsync();
            // var table = await App.Database.GetWorkoutExerciseAsync();
            exercisedatagrid.ItemsSource = table;
        }
    }
}