using App1.Models;
using App1.ViewModels;
using Newtonsoft.Json;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Forms;

namespace App1.Views
{
    public class MainPageViewModel : BaseViewModel
    {
        // This is just as an example. Do not create a static property in your ViewModel, for other ViewModels to communicate with, in a production app.
        public static List<SelectableData<ExampleData>> SelectedData { get; set; }

        public List<SelectableData<ExampleData>> DataSource { get; set; }

        public MainPageViewModel()
        {
            // Load Data
            SelectedData = new List<SelectableData<ExampleData>>();
            
            //getPerson();
        }
        private async void getPerson()
        {
            SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            conn.CreateTable<Person>();
            HttpClient client = new HttpClient();
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            var httpClient = new HttpClient(new System.Net.Http.HttpClientHandler());
            var response = await httpClient.GetStringAsync("https://admin.santisartwitt.com/Employees/indexListJson");
            Device.BeginInvokeOnMainThread(() =>
            {
            var products = JsonConvert.DeserializeObject<List<Person>>(response);
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
            });
            //SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation);
            
            conn.Close();
        }
        public void OnAppearing()
        {
            DataSource = SelectedData.Where(x => x.Selected).ToList();
            OnPropertyChanged(nameof(DataSource));
        }


        public ICommand SelectCommand
        {
            get
            {
                return new Command( () =>
                {
                    var navPage = new NavigationPage(new TabbedPage1());
                    Application.Current.MainPage = navPage;
                    var selectLIst = App1.Views.TabbedPage1.SelectedData;
                     navPage.PushAsync(new MultiSelect(selectLIst));
                     //navPage.PushAsync(new MultiSelect(SelectedData));
                    
                });
            }

        }

    }
}