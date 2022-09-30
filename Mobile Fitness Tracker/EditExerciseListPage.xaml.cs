﻿using System;
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
        //Methdd Add Exercise and Description to Database and dislpay in the gridview
        async private void BtnAddExercise_Clicked(object sender, EventArgs e)
        {    
            //check if exercise and description is typed and not empty
            if (!string.IsNullOrWhiteSpace(EntrExercise.Text) && !string.IsNullOrWhiteSpace(EntrDescription.Text))
            {
                //Save to database
                await App.Database.SaveExerciseAsync(new ExerciseDBClass
                {
                    //Get user exercise input information to database                   
                    Exercise = EntrExercise.Text,
                    Description = EntrDescription.Text,
                    //Index = datagrid.SelectedIndex.ToString()

                });

                //Clear entry inputs
                EntrExercise.Text = string.Empty;
                EntrDescription.Text = string.Empty;
              //refresh screen
                OnAppearing();

            }
        }
        //Method delete All exercises from the Db and list
        async private void BtnExerciseDelete_Clicked(object sender, EventArgs e)
        {    //call CRUD command to delete all records from database        
            await App.Database.DeleteAllExrcises();
            //refresh datagrid
             OnAppearing();
        }

        //not in use
        private void datagrid_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // Lblgrid.Text = datagrid.SelectedItem.ToString();


        }
       
        //Delete Row method
        async private void BtnClose_Clicked_1(object sender, EventArgs e)
        {
            //get selection to label as reference fro testing
            Lblgrid.Text = UserGlobalVaraibles.cellValue.ToString();          
           
            //delete row call sql query from database class
            await App.Database.DeleteRow();
            //refresh
            OnAppearing();
           
        }

        //not in use
        private void datagrid_CurrentCellActivated(object sender, CurrentCellActivatedEventArgs e)
        {
            /* var dataRow = datagrid.GetRowGenerator().Items.FirstOrDefault(x => x.RowIndex == e.CurrentRowColumnIndex.RowIndex);
             var CellValue = datagrid.GetCellValue(dataRow.RowData, datagrid.Columns[e.CurrentRowColumnIndex.ColumnIndex].MappingName);
             Lblgrid.Text = dataRow.ToString();*/


        }
        
       
        //Method get ID cell value on sellected row
        private void datagrid_GridTapped(object sender, GridTappedEventArgs e)
        {
            foreach (var column in datagrid.Columns)
            {
                //set collumn mapping name as reference
                if (column.MappingName == "Id")
                {                    
                    //get row data
                    var rowData = datagrid.GetRecordAtRowIndex(datagrid.SelectedIndex);                    
                    //assign Id to global varaible
                    UserGlobalVaraibles.cellValue = int.Parse(datagrid.GetCellValue(rowData, column.MappingName).ToString());
                }
            }
          
        }



    }
}