using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.DataGrid;
using Xamarin.Forms.Xaml;
using Syncfusion.SfDataGrid.XForms;

namespace Mobile_Fitness_Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditWorkoutListPage : ContentPage
    {
        public EditWorkoutListPage()
        {
            InitializeComponent();
            //remove navigation bar
            NavigationPage.SetHasNavigationBar(this, false);
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //get all exercise from db
            var table1 = await App.Database.GetExerciseAsync();
            //Populate datagrid with exercise
            exercisedatagrid.ItemsSource = table1;


            var table2 = await App.Database.WorkoutExerciseAsync();
           // var table2 = await App.Database.GetWorkoutExerciseAsync();
            workoutexercisedatagrid.ItemsSource = table2;
        }

        //method add values to WorkoutExercise table on button click
        async private void BtnExerciseAdd_Clicked(object sender, EventArgs e)
        {
            //check is datagrid is not selected display alert
            if (exercisedatagrid.SelectedIndex < 0)
            {
                //display alert to select an exercise on datagrid
                DisplayAlert("Selection Error", "Please select exercise to be added", "Close");
            }
            //if datagrid exercise selected - assing selected row to workout
            else
            {
                //Save to database
                await App.Database.SaveWorkoutExerciseAsync(new WorkoutExerciseDBClass
                {
                    //Get workout and exercise information to database
                    ExerciseId = UserGlobalVaraibles.exerciseIdValue,
                    Workout = UserGlobalVaraibles.workoutcellValue,
                    Exercise = UserGlobalVaraibles.exercisecellValue,
                    Description = UserGlobalVaraibles.exercisedescriptionValue
                    //Index = datagrid.SelectedIndex.ToString()

                });

                //refresh screen
                OnAppearing();
            }

        }


        //method get exercise and description values from datagrid on selection
        private void exercisedatagrid_GridTapped(object sender, Syncfusion.SfDataGrid.XForms.GridTappedEventArgs e)
        {
            foreach (var column in exercisedatagrid.Columns)
            {
                //set collumn mapping exercise Id as reference Id
                if (column.MappingName == "Id")
                {
                    //get row data exercise name
                    var rowData = exercisedatagrid.GetRecordAtRowIndex(exercisedatagrid.SelectedIndex);
                    //assign exercise name to global varaible
                    UserGlobalVaraibles.exerciseIdValue = int.Parse(exercisedatagrid.GetCellValue(rowData, column.MappingName).ToString());
                    //  UserGlobalVaraibles.exercisedescriptionValue = exercisedatagrid.GetCellValue(rowData, column.MappingName).ToString();
                }
                //set collumn mapping name as reference Exercise
                if (column.MappingName == "Exercise")
                {
                    //get row data exercise name
                    var rowData = exercisedatagrid.GetRecordAtRowIndex(exercisedatagrid.SelectedIndex);
                    //assign exercise name to global varaible
                    UserGlobalVaraibles.exercisecellValue = exercisedatagrid.GetCellValue(rowData, column.MappingName).ToString();
                  //  UserGlobalVaraibles.exercisedescriptionValue = exercisedatagrid.GetCellValue(rowData, column.MappingName).ToString();
                }
               //set collumn mapping name as reference Description
                if (column.MappingName == "Description")
                {
                    //get row data exercise name
                    var rowData = exercisedatagrid.GetRecordAtRowIndex(exercisedatagrid.SelectedIndex);
                    //assign exercise name to global varaible                   
                    UserGlobalVaraibles.exercisedescriptionValue = exercisedatagrid.GetCellValue(rowData, column.MappingName).ToString();
                }

            }
            
        }

        //Method delete selected row from WorkoutExercise table on button click
        async private void BtnDeleteWorkoutExercise_Clicked(object sender, EventArgs e)
        {
            //check if workoutexercisedatagrid is not selected display alert
            if (workoutexercisedatagrid.SelectedIndex < 0)
            {
                //display alert to select an exercise on datagrid
                DisplayAlert("Selection Error", "Please select exercise to be deleted", "Close");
            }
            //if datagrid exercise selected - delete selected row
            else
            {
                //call delete row query
                await App.Database.DeleteWorkoutExerciseRow();
                OnAppearing();
            }
        }

        //Method get selected row value from WorkoutExercise Grid on selection (tapped)
        private void workoutexercisedatagrid_GridTapped(object sender, GridTappedEventArgs e)
        {
          
            foreach (var column in workoutexercisedatagrid.Columns)
            {
                //set collumn mapping name as reference Description
                   if (column.MappingName == "Id")
                //if (column.MappingName == "ExerciseId")
                {
                    //get row data exercise name
                    var rowData = workoutexercisedatagrid.GetRecordAtRowIndex(workoutexercisedatagrid.SelectedIndex);
                    //assign exercise name to global varaible                    
                    UserGlobalVaraibles.workoutexercise_Id = int.Parse(workoutexercisedatagrid.GetCellValue(rowData, column.MappingName).ToString());
                    
                }
            }
            

        }

        private void BtnWorkoutClose_Clicked(object sender, EventArgs e)
        {
            //Navigate to  WorkoutList Page
            Navigation.PushAsync(new WorkoutListPage());

        }
    }
}