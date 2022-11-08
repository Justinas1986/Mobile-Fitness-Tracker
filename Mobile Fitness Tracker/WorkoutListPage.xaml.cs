using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.SfDataGrid.XForms;
using Xamarin.Forms;
using Xamarin.Forms.DataGrid;
using Xamarin.Forms.Xaml;
using static SQLite.TableMapping;

namespace Mobile_Fitness_Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutListPage : ContentPage
    {
        public WorkoutListPage()
        {
            InitializeComponent();
            OnAppearing();
            
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //get workouts from db
            var table = await App.Database.GetWorkoutAsync();
            //Populate datagrid with workouts
            workoutdatagrid.ItemsSource = table;
            //clear workout selection on appearance
            UserGlobalVaraibles.workoutcellValue = "";
            //disable edit button on appearance
            BtnWorkoutEdit.IsEnabled = false;
        }

        //Method create a workout on button click
        async private void BtnWorkoutAdd_Clicked(object sender, EventArgs e)
        {
            //check if exercise and description is typed and not empty
            if (!string.IsNullOrWhiteSpace(EntrWorkout.Text))
            {
                //Save to database
                await App.Database.SaveWorkoutAsync(new WorkoutDBClass
                {
                    //Get user exercise input information to database                   
                    Workout = EntrWorkout.Text,                   
                    //Index = datagrid.SelectedIndex.ToString()

                });
                //Clear entry inputs
                EntrWorkout.Text = string.Empty;                
                //refresh screen
                OnAppearing();
            }
            //if workout input is missing display alert
            else
            {
                //Display alert if missing workout entry
                DisplayAlert("Missing  Input", "Please enter workout name", "Close");
            }
        }

        //Method navigate to Edit Workout List Page on button click
        private void BtnWorkoutEdit_Clicked(object sender, EventArgs e)
        {
            //Navigate to Edit Workout List Page
            Navigation.PushAsync(new EditWorkoutListPage());
        }
       

        //Method get workout value and ID from datagrid on touch selection (tapped)
        private void workoutdatagrid_GridTapped(object sender, Syncfusion.SfDataGrid.XForms.GridTappedEventArgs e)
        {
            foreach (var column in workoutdatagrid.Columns)
            {
                //set collumn mapping name as reference
                if (column.MappingName == "Workout")
                {
                    //get row data
                    var rowData = workoutdatagrid.GetRecordAtRowIndex(workoutdatagrid.SelectedIndex);
                    //assign workout to global varaible
                    UserGlobalVaraibles.workoutcellValue = workoutdatagrid.GetCellValue(rowData, column.MappingName).ToString();
                   
                }                //enable Edit button once workout is selected
                BtnWorkoutEdit.IsEnabled = true;
               
                //set collumn mapping name as reference ID
                if (column.MappingName == "Id")
                {
                    //get row data
                    var rowData = workoutdatagrid.GetRecordAtRowIndex(workoutdatagrid.SelectedIndex);
                    //assign Id to global varaible
                    UserGlobalVaraibles.cellValue = int.Parse(workoutdatagrid.GetCellValue(rowData, column.MappingName).ToString());
                }
            }
        }



         //Mehod delete workout from the list on button click
        async private void BtnWorkoutDelete_Clicked(object sender, EventArgs e)
        {
            //check is datagrid is not selected display alert
            if (workoutdatagrid.SelectedIndex < 0)
            {
                //display alert to select an exercise on datagrid
                DisplayAlert("Selection Error", "Please select workout to be deleted", "Close");
            }
            //if datagrid workout selected - delete selected row
            else
            {
                //delete row call sql query from database class
                await App.Database.DeleteWorkoutRow();
                //delete also exercises related to particular workout
                await App.Database.DeleteAllWorkoutExercise();
                //refresh
                OnAppearing();
            }
        }

        private void BtnClose_Clicked(object sender, EventArgs e)
        {
            //Navigate to Exercise page
            Navigation.PushAsync(new ExercisePage());
        }
    }
}