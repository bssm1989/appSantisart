using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Services;
using App1.Views;
using App1.Models;
using System.IO;

namespace App1
{
    public partial class App : Application
    {
        //https://xamarinmonkeys.blogspot.com/2019/02/xamarinforms-sqlite-database-crud.html


        public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
            DatabaseLocation = databaseLocation;
        }
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
            //TabbedPage1 = NavPage;
        }
        public static string DatabaseLocation = string.Empty;
        //public App(string databaseLocation)
        //{
        //    InitializeComponent();

        //    MainPage = new NavigationPage(new TabbedPage1());
        //    //DatabaseLocation = databaseLocation;
        //}
        
        //public static s { get; set; }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
