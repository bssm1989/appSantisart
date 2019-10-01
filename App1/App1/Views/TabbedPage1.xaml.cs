using App1.Models;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    public partial class TabbedPage1 : ContentPage
    {
        public static List<SelectableData<ExampleData>> SelectedData { get; set; }
        private List<Product> productArrayList;
      

        public List<SelectableData<ExampleData>> DataSource { get; set; }
        public TabbedPage1()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
            SelectedData = new List<SelectableData<ExampleData>>()
            {
                new SelectableData<ExampleData>() {

                    Data = new ExampleData() { Name = "Test1", Description = "Description1" }
                },
                new SelectableData<ExampleData>() { Data = new ExampleData() { Name = "Test2", Description = "Description2" } },
                new SelectableData<ExampleData>() { Data = new ExampleData() { Name = "Test3", Description = "Description3" } },
                new SelectableData<ExampleData>() { Data = new ExampleData() { Name = "Test4", Description = "Description4" } },
                new SelectableData<ExampleData>() { Data = new ExampleData() { Name = "Test5", Description = "Description5" } }
            };

            getPerson();
        }
        private async void getPerson()
        {
            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.CreateTable<Person>();
            HttpClient client = new HttpClient();
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var httpClient = new HttpClient(new System.Net.Http.HttpClientHandler());
            var response = await httpClient.GetStringAsync("https://admin.santisartwitt.com/Employees/indexListJson");
            var products = JsonConvert.DeserializeObject<List<Person>>(response);
            //SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.CreateTable<Person>();
            foreach (var item in products)
            {

                conn.Insert(item);
            }
            var posts = conn.Table<Person>().ToList();
            if (posts != null)
            {
                SelectedData = new List<SelectableData<ExampleData>>();
                foreach (var item in posts)
                {
                    SelectedData.Add(new SelectableData<ExampleData>()
                    {
                        Data = new ExampleData() { Name = item.EmpName, Description = item.EmpName }
                    });
                }
            }







            conn.Close();
        }
        private async void NavigateButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewItemPage());
        }


        protected async override void OnAppearing()
        {
            base.OnAppearing();
            DataSource = SelectedData.Where(x => x.Selected).ToList();
            OnPropertyChanged(nameof(DataSource));
            ((MainPageViewModel)BindingContext).OnAppearing();

       
            //var personList = await App.SQLiteDb.GetItemsAsync();
            //if (personList != null)
            //{
            //    lstPersons.ItemsSource = personList;
            //}


        }
        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MultiSelect(SelectedData));
        }



        //private async void BtnAdd_Clicked(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txtName.Text))
        //    {
        //        Person person = new Person()
        //        {
        //            Name = txtName.Text
        //        };
        //        SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
        //        conn.CreateTable<Person>();
        //        //Add New Person
        //        await conn.SaveItemAsync(person);
        //        txtName.Text = string.Empty;
        //        await DisplayAlert("Success", "Person added Successfully", "OK");
        //        //Get All Persons
        //        var personList = await App.SQLiteDb.GetItemsAsync();
        //        if (personList != null)
        //        {
        //            lstPersons.ItemsSource = personList;
        //        }
        //    }
        //    else
        //    {
        //        await DisplayAlert("Required", "Please Enter name!", "OK");
        //    }
        //}

        //private async void BtnRead_Clicked(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txtPersonId.Text))
        //    {
        //        //Get Person
        //        var person = await App.SQLiteDb.GetItemAsync(Convert.ToInt32(txtPersonId.Text));
        //        if (person != null)
        //        {
        //            txtName.Text = person.Name;
        //            await DisplayAlert("Success", "Person Name: " + person.Name, "OK");
        //        }
        //    }
        //    else
        //    {
        //        await DisplayAlert("Required", "Please Enter PersonID", "OK");
        //    }
        //}

        //private async void BtnUpdate_Clicked(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txtPersonId.Text))
        //    {
        //        Person person = new Person()
        //        {
        //            PersonID = Convert.ToInt32(txtPersonId.Text),
        //            Name = txtName.Text
        //        };

        //        //Update Person
        //        await App.SQLiteDb.SaveItemAsync(person);

        //        txtPersonId.Text = string.Empty;
        //        txtName.Text = string.Empty;
        //        await DisplayAlert("Success", "Person Updated Successfully", "OK");
        //        //Get All Persons
        //        var personList = await App.SQLiteDb.GetItemsAsync();
        //        if (personList != null)
        //        {
        //            lstPersons.ItemsSource = personList;
        //        }

        //    }
        //    else
        //    {
        //        await DisplayAlert("Required", "Please Enter PersonID", "OK");
        //    }
        //}

        //private async void BtnDelete_Clicked(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(txtPersonId.Text))
        //    {
        //        //Get Person
        //        var person = await App.SQLiteDb.GetItemAsync(Convert.ToInt32(txtPersonId.Text));
        //        if (person != null)
        //        {
        //            //Delete Person
        //            await App.SQLiteDb.DeleteItemAsync(person);
        //            txtPersonId.Text = string.Empty;
        //            await DisplayAlert("Success", "Person Deleted", "OK");

        //            //Get All Persons
        //            var personList = await App.SQLiteDb.GetItemsAsync();
        //            if (personList != null)
        //            {
        //                lstPersons.ItemsSource = personList;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        await DisplayAlert("Required", "Please Enter PersonID", "OK");
        //    }
        //}
    }

    internal class Product
    {
        public string Name { get; set; }
    }


}