using Plugin.Media.Abstractions;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using static System.Net.WebRequestMethods;

namespace Mobile_Fitness_Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateUserPage : ContentPage
    {
        public CreateUserPage()
        {
            InitializeComponent();
            
        }
        public string name;
        public string Firstname;
       // public string Lastname;

        async private void BtnCreateUser_Clicked(object sender, EventArgs e)
        {           
            //check if all entries are filled
            if (!string.IsNullOrWhiteSpace(EntrFirstName.Text) && !string.IsNullOrWhiteSpace(EntrLastName.Text) && !string.IsNullOrWhiteSpace(EntrPreferredName.Text) &&
                !string.IsNullOrWhiteSpace(EntrWeight.Text) && !string.IsNullOrWhiteSpace(EntrHeight.Text) && !string.IsNullOrWhiteSpace(EntrAge.Text) && EntrAge.Text!=("."))
            {
                //delete records from database every time on click (refresh with new data)
                await App.Database.DeleteAll();
                //Save user info in to database
                await App.Database.SavePersonAsync(new UserDBClass
                {
                    //Get user profile input information to database
                    FirstName = EntrFirstName.Text,
                    LastName = EntrLastName.Text,
                    PrefferedName = EntrPreferredName.Text,
                    Weight = double.Parse(EntrWeight.Text),
                    Height = double.Parse(EntrHeight.Text),
                    Age = int.Parse(EntrAge.Text),
                    ProfilePic = name,
                    //Calculate BMI and pass value to database
                    BMI = Math.Round(703 * double.Parse(EntrWeight.Text) / Math.Pow(12 * double.Parse(EntrHeight.Text), 2), 2)
                    
            });

                //Clear entry inputs
                /*  EntrFirstName.Text = string.Empty;
                  EntrLastName.Text = string.Empty;
                  EntrPreferredName.Text = string.Empty;
                  EntrWeight.Text = string.Empty;
                  EntrHeight.Text = string.Empty;
                  EntrAge.Text = string.Empty;*/
                //get first name
                UserGlobalVaraibles.FirstName = EntrFirstName.Text;
                //Navigate to MyProfilePage
                await Navigation.PushAsync(new MyProfilePage());

            }
            //check if there are empty input fields
            else
            {
                //display alert
                DisplayAlert("Invalid Input", "Please fill entry fields", "Close");
            }

        }

        async private void BtnImageUpload_Clicked(object sender, EventArgs e)
            {
                 try
                 {
                     //get a picture from gallery and resize
                      var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions() { PhotoSize = PhotoSize.MaxWidthHeight, MaxWidthHeight = 600 });
                       name = file.Path;
                  //set picture path to a global variable
                   UserGlobalVaraibles.ProfilePic = file.Path;

                ImgProfile.Source = ImageSource.FromStream(() =>
                     {
                         var stream = file.GetStream();
                         file.Dispose();
                         return stream;
                     });

                 }
                 catch (Exception ex)
                 {
                     DisplayAlert("Something went wrong", ex.Message, "Close");
                 }
        }
        //Navigate back to My profile Pge
        private void Btnback_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MyProfilePage());
        }
    }
}