using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Notes
{
    public partial class App : Application
    {
        public const string DBFILENAME = "notes.db";
        public App()
        {
            InitializeComponent();
            //получаем путь к базе данных
            string dbPath = DependencyService.Get<IPath>().GetDatabasePath(DBFILENAME);
            using (var db = new ApplicationContext(dbPath))
            {   
                //создаем новую бд если она отсутсвует
                db.Database.EnsureCreated();
            }

                MainPage = new NavigationPage(new MainPage());
        }

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
