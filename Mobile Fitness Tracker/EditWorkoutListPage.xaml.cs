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
            OnAppearing();
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
            //Save to database
            await App.Database.SaveWorkoutExerciseAsync(new WorkoutExerciseDBClass
            {
                //Get workout and exercise information to database                   
                Workout = UserGlobalVaraibles.workoutcellValue,
                Exercise = UserGlobalVaraibles.exercisecellValue,
                Description =  UserGlobalVaraibles.exercisedescriptionValue
                //Index = datagrid.SelectedIndex.ToString()

            });

            //refresh screen
            OnAppearing();

        }


        //method get exercise and description values from datagrid on selection
        private void exercisedatagrid_GridTapped(object sender, Syncfusion.SfDataGrid.XForms.GridTappedEventArgs e)
        {
            foreach (var column in exercisedatagrid.Columns)
            {
               
                //set collumn mapping name as reference Exercise
                if (column.MappingName == "Exercise")
                {
                    //get row data exercise name
                    var rowData = exercisedatagrid.GetRecordAtRowIndex(exercisedatagrid.SelectedIndex);
                    //assign exercise name to global varaible
                    UserGlobalVaraibles.exercisecellValue = exercisedatagrid.GetCellValue(rowData, column.MappingName).ToString();
                    UserGlobalVaraibles.exercisedescriptionValue = exercisedatagrid.GetCellValue(rowData, column.MappingName).ToString();
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
            await App.Database.DeleteWorkoutExerciseRow();
            OnAppearing();
        }

        //Method delete selected row from WorkoutExercise Grid
        private void workoutexercisedatagrid_GridTapped(object sender, GridTappedEventArgs e)
        {
          
            foreach (var column in workoutexercisedatagrid.Columns)
            {
                //set collumn mapping name as reference Description
                if (column.MappingName == "Id")
                {
                    //get row data exercise name
                    var rowData = workoutexercisedatagrid.GetRecordAtRowIndex(workoutexercisedatagrid.SelectedIndex);
                    //assign exercise name to global varaible                    
                    UserGlobalVaraibles.workoutexercise_Id = int.Parse(workoutexercisedatagrid.GetCellValue(rowData, column.MappingName).ToString());
                    
                }
            }
            

        }


    }
}