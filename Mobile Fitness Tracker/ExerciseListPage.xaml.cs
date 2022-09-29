using System;
using System.Collections.Generic;
using System.ComponentModel;
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
         //   BindingContext = new MainPageViewModel();

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //Populate collectionview1 with user information from database
            var table = await App.Database.GetExerciseAsync();



            // RefreshCommand = new Command(CmdRefresh);

            // ListExercise.ItemsSource = table;

            //List<UserDBClass> AllProducts = (from prod in table //products instance
                        //                     select prod).ToList();
            datagrid.ItemsSource = table;

        }



        private void BtnExerciseEdit_Clicked(object sender, EventArgs e)
        {
            //Navigate to Edit Exercise List Page
            Navigation.PushAsync(new EditExerciseListPage());
        }

        private void BtnExerciseClose_Clicked(object sender, EventArgs e)
        {

        }
    }
}