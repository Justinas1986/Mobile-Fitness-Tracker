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
           
            //scheduledatagrid.ItemsSource
           // lbldate.Text=scheduledatagrid.ItemsSource.ToString();
        }
       

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //get workouts list from db
            var workouttable = await App.Database.GetWorkoutListAsync();
                 
            //run method to get values to the schedule table and notification
            Notification();
            //populate picker with workout values
            PckWorkout.ItemsSource = workouttable;

            //clear workout selection on appearance
            UserGlobalVaraibles.workoutcellValue = "";
            //current date and time to label for testing
            // lbldate.Text = PckTime.Time.ToString(@"hh\:mm");//android time format!!!!!
          //  lbldate.Text = UserGlobalVaraibles.testingvalue;

           

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


         public static void MyNotification()
         {
            // var Notification = new WorkoutSchedulePage();
            //Notification.Notification();

              WorkoutSchedulePage MyInstance = new WorkoutSchedulePage();
              MyInstance.Notification();
         }


        public  static void Message()
        {
          //your instance of the method here
        }

        public void MyMessage()
        {
           //your logic here
           

        }
       
        //method get notification
        async public void Notification()
        {
           
          //  WorkoutSchedulePage wSPage = new WorkoutSchedulePage();
            //Get workouts into variable
            var table = await App.Database.GetWorkoutScheduleAsync();
            //Populate datagrid with workouts
            scheduledatagrid.ItemsSource = table;
            //get row data exercise name
            //var rowData = scheduledatagrid.GetRecordAtRowIndex(1);

            // lbldate.Text = rowData.ToString();

            //check if datagrid is not empty !="NaN"
            if (scheduledatagrid.GetRecordAtRowIndex(1).ToString() != "NaN")
            {
                //start foreground service
                StartForegroundService();


                //loop to read from variable table
                foreach (var v in table)
                {
                    //if date and time from the schedule table matches date and time value now 
                    if ((v.Date == DateTime.Now.ToString("ddd d MMM")) && (v.Time == DateTime.Now.ToString("HH:mm")))
                    {
                        DisplayAlert("Date and time", "Time for a workout now", "OK");

                        /* string time = "";
                         //notification initiation
                         var notification = new NotificationRequest
                         {                        //properties
                             BadgeNumber = 1,
                             Description = "Testing",
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
                         await LocalNotificationCenter.Current.Show(notification);*/
                        // lbldate.Text = "OK got date and time";//for testing
                    }

                }


            }
            //if datagrid table is empty (no scheduled workouts)
            else
            {
                //Display alert
                DisplayAlert("Empty Table", "Please schedule workouts", "OK");
                //start foreground service
                StopForegroundService();
                //return;
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
                        //refresh
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
                //refresh.
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
        //method start foreground service
      public void StartForegroundService()
        {
           
            lbldate.Text = "service is running";
            //if service is running show alert message
            if (DependencyService.Resolve<IForegroundService>().IsForeGroundServiceRunning())
            {
                App.Current.MainPage.DisplayAlert("Opps", "Foreground Service is already running", "OK");
            }
            //if not running start the service
            else
            {
                DependencyService.Resolve<IForegroundService>().StartMyForegroundService();

            }
        }
        //method stop foreground service
        public void StopForegroundService()
        {
            DependencyService.Resolve<IForegroundService>().StopMyForegroundService();

        }



    }
}