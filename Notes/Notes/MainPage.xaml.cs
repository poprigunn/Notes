using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;

namespace Notes
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            Title = "Заметки";
        }

        protected override void OnAppearing()
        {
            //задаем платформенные спецификации для Windows
            WindowsSpecific();
            //получаем бд и записываем данные в лист
            string dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
            //работа с бд
            using (ApplicationContext db = new ApplicationContext(dbPath))
            {
                notesList.ItemsSource = db.Notes.ToList();
            }
                base.OnAppearing();
        }

        //обработка нажатия выбранного элемента в листе
        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Note selectedNote = (Note)e.SelectedItem;
            NotePage notePage = new NotePage();
            notePage.BindingContext = selectedNote;
            await Navigation.PushAsync(notePage);
        }

        //обработка нажатия кнопки создания новой заметки
        private async void CreateNote(object sender, EventArgs e)
        {
            Note note = new Note();
            NotePage notePage = new NotePage();
            notePage.BindingContext = note;
            await Navigation.PushAsync(notePage);
        }

        private void WindowsSpecific()
        {
            notesList.On<Windows>().SetSelectionMode(Xamarin.Forms.PlatformConfiguration.WindowsSpecific.ListViewSelectionMode.Inaccessible);
        }
    }
}
