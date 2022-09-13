using Shopping_App.Data;
using System;
using System.IO;
using Xamarin.Forms;

namespace Shopping_App
{
    public partial class App : Application
    {
        static MyDatabase database;
        // Create the database connection as a singleton.
        public static MyDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new MyDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyData.db"));
                }
                return database;
            }
        }

        public static string FolderPath { get; internal set; }

        public App()
        {
            InitializeComponent();
            DependencyService.Register<MyDatabase>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
