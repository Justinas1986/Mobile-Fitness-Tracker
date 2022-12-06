using Android.Views.Animations;
using Java.Util;
using SQLite;
using Syncfusion.Data;
using Syncfusion.SfDataGrid.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.DataGrid;
using Xamarin.Forms.Xaml;
using static Android.Content.ClipData;
using static Java.Util.Jar.Attributes;
using static SQLite.TableMapping;

namespace Mobile_Fitness_Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutStartPage : ContentPage
    {
        //Get workout start time
        public DateTime starttime= DateTime.Now;

        public WorkoutStartPage()
        {
            InitializeComponent();
            //remove navigation bar
            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            GetExecises();
            

          //   IsWorkoutEmpty();
        }     

        async public void GetExecises()
        {
            //get exercise from db related for particular workout
            var table = await App.Database.WorkoutExerciseAsync();
            // var table = await App.Database.GetWorkoutExerciseAsync();
            exercisedatagrid.ItemsSource = table;
            //check if workout list is not empty
            IsWorkoutEmpty();

        }

     

        private void exercisedatagrid_GridTapped(object sender, Syncfusion.SfDataGrid.XForms.GridTappedEventArgs e)
        {
           /* IsAllWorkoutsDone();
             lbldate.Text = "tapped";                  */
                       
        }

        //method check if a workout is complete
        async public void IsAllWorkoutsDone()
        {
            var table = await App.Database.WorkoutExerciseAsync();
            //local variable for index calc
            int index = 0;

            //cycle to read all exercises
            foreach (var w in table)
            { 
                //increase index value
                index++;
                //get row data exercise name
                var rowData = exercisedatagrid.GetRecordAtRowIndex(index);
                //assign exercise name to global varaible
                var ExerciseCheck = (exercisedatagrid.GetCellValue(rowData, "ck").ToString());
                // UserGlobalVaraibles.exerciseCkValue = (exercisedatagrid.GetCellValue(rowData, "ck").ToString());
                

                //check if any exercise is incomplete
                if (ExerciseCheck == "False")               
                {                 
                    //display message about incomplete exercise
                    DisplayAlert("Incomplete Workout", $"Please finish your {w.Exercise} {w.Description}", "OK");
                    //return no value
                    return;
                }             
            }
            //Get workout end time
            DateTime endtime = DateTime.Now;
            //Calculate workout duration time         
            TimeSpan ts = endtime - starttime;
                       
            //if all exercises are done show message
            DisplayAlert("Great Job!", $"Workout is complete in {ts.Minutes} minutes. Press Close button to return to the Schedule page.", "OK");
            //Navigate to Workout Start page
            //Navigation.PushAsync(new WorkoutSchedulePage());
        }          

    

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            lbldate.Text = "checked";
        }
        //method confirm workout is complete on button click
        private void BtnDone_Clicked(object sender, EventArgs e)
        {
            //call method
            IsAllWorkoutsDone();
           

            //delete accomplished workout
            //App.Database.DeleteWorkoutScheduleRow();

        }      

        //method check if exercise list is not empty
      async  public void IsWorkoutEmpty()
        {          
            //check if datagrid is not empty
            if (exercisedatagrid.View.Records.Count()<1)
            {
                //display message
               await DisplayAlert("Missing exercises", $"Please assign exercises to the '{UserGlobalVaraibles.workoutcellValue}' workout!", "OK");
                //Navigate to Workout Start page
                Navigation.PushAsync(new WorkoutSchedulePage());
            }
        
        }
        //method navigate to WorkoutSchedulePage on button click
        async private void BtnClose_Clicked(object sender, EventArgs e)
        {
            //display alert 
            bool answer = await DisplayAlert("Exit workout?", $"Do you want to exit the workout '{UserGlobalVaraibles.workoutcellValue}?", "Yes", "No");
            //if Yes 
            if (answer == true)
            {
                //Navigate to Workout Start page
                Navigation.PushAsync(new WorkoutSchedulePage());
            }
        }
    }
}