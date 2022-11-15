using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using Mobile_Fitness_Tracker.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: Xamarin.Forms.Dependency(typeof(ForegroundService))]
namespace Mobile_Fitness_Tracker.Droid
{
    
     [Service]
       public class ForegroundService : Service, IForegroundService
       {
           public static bool IsForegroundServiceRunning;

           public override IBinder OnBind(Intent intent)
           {
               throw new NotImplementedException();
           }
           [return: GeneratedEnum]
           public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
           {
           
            //write the logic here        
          
             Task.Run(async () =>
               {
                   while (IsForegroundServiceRunning)
                   {
                       Device.BeginInvokeOnMainThread(async () =>
                       {
                           // call notification method
                           WorkoutSchedulePage.Notification();
                           
                           // await App.Current.MainPage.DisplayAlert("Time", "Time for workour now!", "OK");
                       });                  
                                         
                       //display message in debug window
                       System.Diagnostics.Debug.WriteLine("Backgroud Service is Running");
                       //delay function
                       Thread.Sleep(60000); //every 60s
                      
                   }
             });
            
               //Create notofication channel
               string channelID = "ForegroundServiceChannel";
               var notificationManager = (NotificationManager)GetSystemService(NotificationService);
               if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
               {
                   var notificationChannel = new NotificationChannel(channelID, channelID, NotificationImportance.Max);
                   notificationManager.CreateNotificationChannel(notificationChannel);
               }
            //pas notification builder
            var notificationBuilder = new NotificationCompat.Builder(this, channelID)
                   .SetContentTitle("Workout has been scheduled!")
                   .SetSmallIcon(Resource.Mipmap.icon)
                   .SetContentText("Notification will appear on your scheduled time.")
                   .SetPriority(1)
                   .SetOngoing(true)
                   .SetChannelId(channelID)
                   .SetAutoCancel(true);
         
               //start foreground service
               StartForeground(1001, notificationBuilder.Build());        
               return base.OnStartCommand(intent, flags, startId);


           }
           //methods to control background process
           public override void OnCreate()
           {
               //when the service is runnin set to True
               base.OnCreate();
               IsForegroundServiceRunning = true;
           }

           public override void OnDestroy()
           {
               //when the service is Not runnin set to false
               base.OnDestroy();
               IsForegroundServiceRunning = false;
           }

           public void StartMyForegroundService()
           {
               var intent = new Intent(Android.App.Application.Context, typeof(ForegroundService));
               Android.App.Application.Context.StartForegroundService(intent);
               //return true;
           }

           public void StopMyForegroundService()
           {
               var intent = new Intent(Android.App.Application.Context, typeof(ForegroundService));
               Android.App.Application.Context.StopService(intent);
               // return true;
           }

           public bool IsForeGroundServiceRunning()
           {
               return IsForegroundServiceRunning;
           }
       }
}