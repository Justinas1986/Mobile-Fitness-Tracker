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
            //get workouts from db
            var table = await App.Database.GetWorkoutAsync();
            //Populate datagrid with workouts
            scheduledatagrid.ItemsSource = table;
            //clear workout selection on appearance
            UserGlobalVaraibles.workoutcellValue = "";
            //current date and time
            lbldate.Text = DateTime.Now.ToString("ddd d MMM");


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
            //check if datagrid is not selected display alert
            if (scheduledatagrid.SelectedIndex > -1)
            {
                //check if set date is not less than current date
                if (DateTime.Now <= PckDate.Date)
                {
                    //check if picker date and tipe is set
                    if (!string.IsNullOrWhiteSpace(PckDate.ToString()) || !string.IsNullOrWhiteSpace(PckTime.ToString()))
                    {
                        //assign date to global variable (date format d)
                        UserGlobalVaraibles.Date = PckDate.Date.ToString("ddd d MMM");
                        //assign time to global variable
                        UserGlobalVaraibles.Time = PckTime.Time.ToString();
                        //call workout update query to add date and time to selected workout
                        await App.Database.WorkoutUpdateAsync();
                        UserGlobalVaraibles.workoutcellValue = null;
                        //refresh screen
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
    }
}