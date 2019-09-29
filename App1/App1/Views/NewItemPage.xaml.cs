using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using App1.Models;
using SQLite;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;

namespace App1.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Text = "Item name",
                Description = "This is an item description."
            };

            BindingContext = this;
            getPerson();

        }
        public static List<SelectableData<ExampleData>> SelectedData { get; set; }
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
        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new MultiSelect(SelectedData));
        }
        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        private async void NavigateButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListViewPage2());
        }
    }
}