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
using Android.App;
using Plugin.LocalNotification.AndroidOption;
using Android.Media;
using Android.Views;
using Android.OS;
using Android.Graphics.Drawables;
using Java.Nio.FileNio.Attributes;
using Java.IO;
using Android.Text.Format;
using Java.Lang;
using Android.Content;
using Android.Runtime;
using Android.Content.PM;

using  Mobile_Fitness_Tracker;
using Android;



[assembly: Xamarin.Forms.Dependency(typeof(ForegroundService))]
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
            //populate pi ker with workouts
            PopulatePicker();
            //run method to get values to the schedule table and notification
            Schedule();        
                      
            //current date and time to label for testing
            // lbldate.Text = PckTime.Time.ToString(@"hh\:mm");//android time format!!!!!
               

        }
              
        //Method get workout values from database and populate to picker
       async public void PopulatePicker()
        {
            //get workouts list from db
            var workouttable = await App.Database.GetWorkoutListAsync();
            //populate picker with workout values
            PckWorkout.ItemsSource = workouttable;
            //clear workout selection on appearance
            UserGlobalVaraibles.workoutcellValue = "";
        }


        //Method get workout value from datagrid on touch selection (tapped)
        private void workoutdatagrid_GridTapped(object sender, Syncfusion.SfDataGrid.XForms.GridTappedEventArgs e)
        {
            //get table values in to variable
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
        //instance of notifitaion method
         public  static void Notification()
         {
            //your instance of the method here
            WorkoutSchedulePage MyInstance = new WorkoutSchedulePage();
            MyInstance.MyNotification();
            
        }
        //method to show notification when the time is for the workout
       async public void MyNotification()
         {
          
            //Get workouts into variable from database
            var table = await App.Database.GetWorkoutScheduleAsync();
                    
            //loop to read from variable table           
            foreach (var v in table)
            {
                //if date and time from the schedule table matches date and time value now 
                if ((v.Date == DateTime.Now.ToString("ddd d MMM")) && (v.Time == DateTime.Now.ToString("HH:mm")))
                {
                    App.Current.MainPage.DisplayAlert("Date and time", "Time for a workout now", "OK");

                    // string time = "";
                        //notification initiation
                        var notification = new NotificationRequest
                        {                        //properties
                            BadgeNumber = 1,
                            Description = "Today is your " + $"{v.Workout}"+ " workout",
                            Title = "Time for exercise" + " " + $"{v.Date}" + " " + $"{v.Time}",
                            ReturningData = "Dummy duomenys",
                            NotificationId = 123,
                            Schedule = { NotifyTime = DateTime.Now }, // Used for Scheduling local notification, if not specified notification will show immediately.               

                            Android = new Plugin.LocalNotification.AndroidOption.AndroidOptions()
                            {

                                //Importance = Plugin.LocalNotification.AndroidOption.AndroidImportance.High,
                                Priority = Plugin.LocalNotification.AndroidOption.AndroidPriority.High,
                                //  Priority = Plugin.LocalNotification.AndroidOption.AndroidImportance.High,
                                VisibilityType = Plugin.LocalNotification.AndroidOption.AndroidVisibilityType.Public,
                            }
                        };
                        //show notification
                        await LocalNotificationCenter.Current.Show(notification);

                }
                /* else
                 {                   
                     App.Current.MainPage.DisplayAlert("Time not found", "Time not found", "OK");
                     //return;
                 }
                 return;*/
            }

        }
    

        //method get populate griwdview with wotkout schedule info and start backgroud service for notification
        async public void Schedule()
        {
            //Get workouts into variable from database
            var table = await App.Database.GetWorkoutScheduleAsync();
            //Populate datagrid with workouts
            scheduledatagrid.ItemsSource = table;

            //check if datagrid is not empty !="NaN"
            if (scheduledatagrid.GetRecordAtRowIndex(1).ToString() != "NaN")
            {
                //start foreground service
                StartForegroundService();            

            }
            //if schedule table is empty
            else
            {
                //stop foreground service
                StopForegroundService();
                DisplayAlert("Missing Schedule", "Please schedule a workout", "Close");
            }
          
        }


        //Method schedule a workout time on button click
        async private void BtnWorkoutSchedule_Clicked(object sender, EventArgs e)
        {            
            //check if the workoutt if selected
            if (PckWorkout.SelectedIndex > -1)
            {
                //check if set date is not less than current date
                if (DateTime.Now > PckDate.Date )
                {
                    //check if picker date and tipe is set
                    if (!string.IsNullOrWhiteSpace(PckDate.ToString()) || !string.IsNullOrWhiteSpace(PckTime.ToString()))
                    {                                           
                          //Save to database
                        await App.Database.SaveWorkoutScheduleAsync(new WorkoutScheduleDBClass
                        {
                            //Get workout and exercise information to database
                            Workout = lbldate.Text,     // get workout value from label                      
                            Date = PckDate.Date.ToString("ddd d MMM"),//get date value from picker                            
                            Time = PckTime.Time.ToString(@"hh\:mm"),//get time value from picker
                        }) ;
                        //Populate gridview and start background notification
                        Schedule();                        
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

  

        //method in use to cast value from picker to label
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
                //refresh.
                Schedule();
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
        //method start foreground service
      public void StartForegroundService()
        {          
         
            //if service is not running, start service
            if (DependencyService.Resolve<IForegroundService>().IsForeGroundServiceRunning()==false)
            {
                //App.Current.MainPage.DisplayAlert("Opps", "Foreground Service is already running", "OK");
                //start service
                DependencyService.Resolve<IForegroundService>().StartMyForegroundService();
            }
            //if not running start the service
           /* else
            {
                DependencyService.Resolve<IForegroundService>().StartMyForegroundService();

            }*/
        }
        //method stop foreground service
        public void StopForegroundService()
        {
            DependencyService.Resolve<IForegroundService>().StopMyForegroundService();

        }

    }
}