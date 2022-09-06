using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;


namespace Mobile_Fitness_Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyProfilePage : ContentPage
    {
        public MyProfilePage()
        {
            InitializeComponent();
            
        }

        private void BtnClose_Clicked(object sender, EventArgs e)
        {
            //Return back to MainPage
            Application.Current.MainPage.Navigation.PopAsync();

        }
        //youtube.com/watch?v=DJYLrVNY2ak
        /// <summary>
        /// Picture Upload Method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BtnUploadPic_Clicked(object sender, EventArgs e)
        {
            try
            {
                //get a picture from gallery and resize
                var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions() { PhotoSize = PhotoSize.MaxWidthHeight, MaxWidthHeight = 600 });

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