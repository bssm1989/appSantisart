using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace App1.Views
{
    internal class MultiSelectViewModel
    {
        private List<SelectableData<ExampleData>> SelectedData;

        //private List<SelectableData<ExampleData>> data;

        public MultiSelectViewModel(List<SelectableData<ExampleData>> data)
        {
            DataList = data;
        }
        public List<SelectableData<ExampleData>> DataList { get; set; }
        public List<SelectableData<ExampleData>> GetNewData()
        {
            var list = new List<SelectableData<ExampleData>>();

            foreach (var data in DataList)
                list.Add(new SelectableData<ExampleData>() {
                    Data = data.Data.Clone(), Selected = data.Selected
                });

            return list;
        }

        public ICommand FinishCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var navPage = new NavigationPage(new TabbedPage1());
                    Application.Current.MainPage = navPage;

                    //await navPage.PushAsync(new MultiSelect(SelectedData));
                    MainPageViewModel.SelectedData = GetNewData();
                    await navPage.PopAsync();
                });
            }

        }

    }
}