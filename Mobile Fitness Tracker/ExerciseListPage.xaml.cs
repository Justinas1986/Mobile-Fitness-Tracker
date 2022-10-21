using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;



namespace Mobile_Fitness_Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExerciseListPage : ContentPage
    {
        
        public ExerciseListPage()
        {
            InitializeComponent();
            OnAppearing();
        

        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //Populate collectionview1 with user information from database
            var table = await App.Database.GetExerciseAsync();
                       
            datagrid.ItemsSource = table;       

         }



        private void BtnExerciseEdit_Clicked(object sender, EventArgs e)
        {
            //Navigate to Edit Exercise List Page
            Navigation.PushAsync(new EditExerciseListPage());
        }

     
    }
}