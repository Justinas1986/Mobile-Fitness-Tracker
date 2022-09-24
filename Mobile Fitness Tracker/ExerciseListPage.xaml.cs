using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Mobile_Fitness_Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExerciseListPage : ContentPage
    {
        public ExerciseListPage()
        {
            InitializeComponent();
            /*List<User> items = new List<User>();
            items.Add(new User() { Name = "John Doe", Age = 42, Mail = "john@doe-family.com" });
            items.Add(new User() { Name = "Jane Doe", Age = 39, Mail = "jane@doe-family.com" });
            items.Add(new User() { Name = "Sammy Doe", Age = 7, Mail = "sammy.doe@gmail.com" });
            lvUsers.ItemsSource = items;*/
            OnAppearing();
          

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //Populate collectionview1 with user information from database
               var table = await App.Database.GetPeopleAsync();
          
            
                ListExercise.ItemsSource = table;
               

                //get Profile picture from DB to global variable
            
          
            

        }


        private void BtnExerciseEdit_Clicked(object sender, EventArgs e)
        {

        }

        private void BtnExerciseClose_Clicked(object sender, EventArgs e)
        {

        }
    }
}