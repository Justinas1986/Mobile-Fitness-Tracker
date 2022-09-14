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

        async private void BtnCreateUser_Clicked(object sender, EventArgs e)
        {
            //string name = file.Path;
            //delete records from database every time on click
            await App.Database.DeleteAll();
            if (!string.IsNullOrWhiteSpace(EntrFirstName.Text))
            {
                await App.Database.SavePersonAsync(new UserDBClass
                {
                    
                FirstName = EntrFirstName.Text,
                    LastName = EntrLastName.Text,
                    PrefferedName = EntrPreferredName.Text,
                    Weight = EntrWeight.Text,
                    Height = EntrHeight.Text,
                    Age = EntrAge.Text,
                    ProfilePic = name
                });

                EntrFirstName.Text = string.Empty;
                EntrLastName.Text = string.Empty;
                EntrPreferredName.Text = string.Empty;

                //collectionView.ItemsSource = await App.Database.GetPeopleAsync();


                await Navigation.PushAsync(new MyProfilePage());


            }
         } 

           async private void BtnImageUpload_Clicked(object sender, EventArgs e)
            {
                 try
                 {
                     //get a picture from gallery and resize
                      var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions() { PhotoSize = PhotoSize.MaxWidthHeight, MaxWidthHeight = 600 });
                       name = file.Path;
                     //Lblpath.Text = name;
               
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



        }
}