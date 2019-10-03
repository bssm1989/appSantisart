using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage2 : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }

        public ListViewPage2(List<SelectableData<ExampleData>> data)
        {
            InitializeComponent();

            BindingContext = new MultiSelectAttendance(data);
            DataList = data;
        }
        public List<SelectableData<ExampleData>> DataList { get; set; }
        public List<SelectableData<ExampleData>> GetNewData()
        {
            var list = new List<SelectableData<ExampleData>>();

            foreach (var data in DataList)
                list.Add(new SelectableData<ExampleData>()
                {
                    Data = data.Data.Clone(),
                    Selected = data.Selected
                });

            return list;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            var navPage = new NavigationPage(new NewItemPage());
            Application.Current.MainPage = navPage;

            //await navPage.PushAsync(new MultiSelect(SelectedData));
            MainPageViewModelAttendance.SelectedData = GetNewData();
            await navPage.PopAsync();
        }

    }
}
