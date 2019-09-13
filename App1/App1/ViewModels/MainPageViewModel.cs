using App1.ViewModels;
using System.Collections.Generic;
using System.Linq;
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
            SelectedData = new List<SelectableData<ExampleData>>()
            {
                new SelectableData<ExampleData>() { Data = new ExampleData() { Name = "Test1", Description = "Description1" } },
                new SelectableData<ExampleData>() { Data = new ExampleData() { Name = "Test2", Description = "Description2" } },
                new SelectableData<ExampleData>() { Data = new ExampleData() { Name = "Test3", Description = "Description3" } },
                new SelectableData<ExampleData>() { Data = new ExampleData() { Name = "Test4", Description = "Description4" } },
                new SelectableData<ExampleData>() { Data = new ExampleData() { Name = "Test5", Description = "Description5" } }
            };
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
                return new Command(async () =>
                {
                    var navPage = new NavigationPage(new TabbedPage1());
                    Application.Current.MainPage = navPage;

                    await navPage.PushAsync(new MultiSelect(SelectedData));
                    
                });
            }

        }

    }
}