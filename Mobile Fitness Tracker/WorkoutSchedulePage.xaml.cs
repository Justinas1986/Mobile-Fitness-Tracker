using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Syncfusion.SfDataGrid.XForms;
using Xamarin.Forms;
using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Syncfusion.Data;
using Xamarin.Forms.DataGrid;
using static System.Net.Mime.MediaTypeNames;
using System.Xml;

namespace Mobile_Fitness_Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkoutSchedulePage : ContentPage
    {
        public WorkoutSchedulePage()
        {
            InitializeComponent();                       

        }
       

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //get workouts list from db
            var workouttable = await App.Database.GetWorkoutListAsync();
            //workouttable.ToArray(String);
            //Populate datagrid with workouts
            var table = await App.Database.GetWorkoutScheduleAsync();
              scheduledatagrid.ItemsSource = table;

            //populate picker with workout values
            //  PckWorkout.ItemsSource = (System.Collections.IList)workouttable;
            PckWorkout.ItemsSource = workouttable;

            //clear workout selection on appearance
            UserGlobalVaraibles.workoutcellValue = "";
            //current date and time
            //  lbldate.Text = DateTime.Now.ToString("ddd d MMM");

            
        }

        //Method get workout value from datagrid on touch selection (tapped)
        private void workoutdatagrid_GridTapped(object sender, Syncfusion.SfDataGrid.XForms.GridTappedEventArgs e)
        {
            foreach (var column in scheduledatagrid.Columns)
            {
                //set collumn mapping name as reference
                if (column.MappingName == "Workout")
                {
                    //get row data
                    var rowData = scheduledatagrid.GetRecordAtRowIndex(scheduledatagrid.SelectedIndex);
                    //assign Id to global varaible
                    UserGlobalVaraibles.workoutcellValue = scheduledatagrid.GetCellValue(rowData, column.MappingName).ToString();
                }                
               
            }           
        }

        //Method schedule a workout time on button click
        async private void BtnWorkoutSchedule_Clicked(object sender, EventArgs e)
        {
            //check if the workoutt if selected
            if (PckWorkout.SelectedIndex > -1)
            {
                //check if set date is not less than current date
                if (DateTime.Now <= PckDate.Date)
                {
                    //check if picker date and tipe is set
                    if (!string.IsNullOrWhiteSpace(PckDate.ToString()) || !string.IsNullOrWhiteSpace(PckTime.ToString()))
                    {
                        string value = lbldate.Text;
                        
                        //Save to database
                        await App.Database.SaveWorkoutScheduleAsync(new WorkoutScheduleDBClass
                        {
                            //Get workout and exercise information to database
                           
                            Workout = lbldate.Text,
                            
                            Date = PckDate.Date.ToString("ddd d MMM"),
                            Time = PckTime.Time.ToString(),
                       

                    });



                        /*//store workoutselection value in to string variable
                        string workout = PckWorkout.SelectedItem.ToString();
                        //assign date to global variable (date format d)
                        UserGlobalVaraibles.Date = PckDate.Date.ToString("ddd d MMM");
                        //assign time to global variable
                        UserGlobalVaraibles.Time = PckTime.Time.ToString();
                        //call workout update query to add date and time to selected workout
                        await App.Database.WorkoutUpdateAsync();
                        UserGlobalVaraibles.workoutcellValue = null;*/
                        //refresh screen
                       
                     //   lbldate.Text = PckWorkout.SelectedItem.ToString();
                        // lbldate.SetBinding(Label.TextColorProperty, new Binding("SelectedItem", source:PckWorkout));
                        OnAppearing();
                        
                    }
                    //if date or time is not set display alert
                    else { DisplayAlert("Missing  Date or Time Input", "Please set date and time", "Close"); }

                }
                //if date less than current date
                else { DisplayAlert("Wrong date set", "Please set future date", "Close"); }
            }
            //if workout is not selected display alert
            else
            {
                //Display alert if workout is not selected
                DisplayAlert("Missing Selection", "Please select a workout", "Close");
            }
            
        }

      async  private void BtnWorkoutClose_Clicked(object sender, EventArgs e)
        {
            
            var notification = new NotificationRequest
            {
                BadgeNumber = 1,
                Description = "Testing",
                Title = "Notification",
                ReturningData = "Dummy duomenys",
                NotificationId = 123,
                Schedule = { NotifyTime = DateTime.Now.AddSeconds(5)}, // Used for Scheduling local notification, if not specified notification will show immediately.
                
                Android = new Plugin.LocalNotification.AndroidOption.AndroidOptions()
                {
                  //  Priority = Plugin.LocalNotification.AndroidOption.AndroidPriority.High,
                    
                }
            };
            await LocalNotificationCenter.Current.Show(notification);


        }
        //in use to cast value from picker to label
        public void PckWorkout_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get picker value to label
             lbldate.SetBinding(Label.TextProperty, new Binding("Workout", source: PckWorkout.SelectedItem));
            
            
        }
        //method delete scheduled workout
        async private void BtnDeleteWorkoutSchedule_Clicked(object sender, EventArgs e)
        {
            //check is datagrid is not selected display alert
            if (scheduledatagrid.SelectedIndex < 0)
            {
                //display alert to select an exercise on datagrid
                DisplayAlert("Selection Error", "Please select workout to be deleted", "Close");
            }
            //if datagrid workout selected - delete selected row
            else
            {
                //delete row call sql query from database class
                await App.Database.DeleteWorkoutScheduleRow();
                //refresh
                OnAppearing();
            }

        }
        //Method get workout value and ID from datagrid on touch selection (tapped)
        private void scheduledatagrid_GridTapped(object sender, GridTappedEventArgs e)
        {
            foreach (var column in scheduledatagrid.Columns)
            {
                //set collumn mapping name as reference
                if (column.MappingName == "Workout")
                {
                    //get row data
                    var rowData = scheduledatagrid.GetRecordAtRowIndex(scheduledatagrid.SelectedIndex);
                    //assign workout to global varaible
                    UserGlobalVaraibles.workoutcellValue = scheduledatagrid.GetCellValue(rowData, column.MappingName).ToString();

                }                
                //set collumn mapping name as reference ID
                if (column.MappingName == "Id")
                {
                    //get row data
                    var rowData = scheduledatagrid.GetRecordAtRowIndex(scheduledatagrid.SelectedIndex);
                    //assign Id to global varaible
                    UserGlobalVaraibles.cellValue = int.Parse(scheduledatagrid.GetCellValue(rowData, column.MappingName).ToString());
                }
      
            }
        }
    }
}