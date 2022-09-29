using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Syncfusion.SfDataGrid.XForms;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.DataGrid;
using Xamarin.Forms.Xaml;

namespace Mobile_Fitness_Tracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditExerciseListPage : ContentPage
    {
        public EditExerciseListPage()
        {
            InitializeComponent();
            OnAppearing();            
        }


        public int digit=0;

    protected override async void OnAppearing()
        {
            base.OnAppearing();
            //get exercises from db
            var table = await App.Database.GetExerciseAsync();
            //Populate datagrid with exercises
            datagrid.ItemsSource = table;
                             

        }

        private void BtnClose_Clicked(object sender, EventArgs e)
        {

        }

        async private void BtnAddExercise_Clicked(object sender, EventArgs e)
        {
            digit++;
            datagrid.SelectedIndex = digit;

            if (!string.IsNullOrWhiteSpace(EntrExercise.Text))
            {

                await App.Database.SaveExerciseAsync(new ExerciseDBClass
                {

                    //Get user exercise input information to database                   
                    Exercise = EntrExercise.Text,
                    Description = EntrDescription.Text,
                    Index = datagrid.SelectedIndex.ToString()
                    
            }) ;

               //Clear entry inputs
               EntrExercise.Text = string.Empty;
               EntrDescription.Text = string.Empty;             
                //  Lblgrid.Text = UserGlobalVaraibles.Id.ToString();
                OnAppearing();
                
            }
        }

        async private void BtnExerciseDelete_Clicked(object sender, EventArgs e)
        {

            //this.dataGrid.SelectItem(firstMarketingItem);
            //  Lblgrid.Text = datagrid.SelectedItem.ToString();
            await App.Database.DeleteAllExrcises();
            datagrid.SelectedItems.Clear();
           // OnAppearing();
        }

        private void datagrid_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Lblgrid.Text = datagrid.SelectedItem.ToString();
        }

        //Delete Row method
        async private void BtnClose_Clicked_1(object sender, EventArgs e)
        {
             
            //get selection
            Lblgrid.Text= UserGlobalVaraibles.selection.ToString();
            //parse to int
            UserGlobalVaraibles.selection = int.Parse(datagrid.SelectedIndex.ToString());
            //delete row
            await App.Database.DeleteRow();      
                     
            OnAppearing();


        }
    }
}