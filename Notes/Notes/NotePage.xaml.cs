using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;

namespace Notes
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotePage : ContentPage
	{
        string dbPath;

		public NotePage ()
		{
			InitializeComponent ();
            Title = "Новая заметка";
        }

        protected override void OnAppearing()
        {
            //задаем платформенные спецификации для Android
            AndroidSpecific();
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
            base.OnAppearing();
        }

        //обработка нажатия кнопки сохранения заметок
        private void SaveNote(object sender, EventArgs e)
        {
            try
            {
                var note = (Note)BindingContext;
                //проверка на заполнение полей названия и текста заметки
                if (!String.IsNullOrEmpty(note.Text) && !String.IsNullOrEmpty(note.Name))
                {
                    //работа с бд
                    using (ApplicationContext db = new ApplicationContext(dbPath))
                    {
                        if (note.Id == 0)
                            db.Notes.Add(note);
                        else
                            db.Notes.Update(note);
                        db.SaveChanges();
                    }
                    this.Navigation.PopAsync();
                }
                else
                {
                    DisplayAlert("Ошибка", "Нужно ввести название и содержимое заметки!", "ОК");
                }
            }
            catch(Exception ex)
            {
                DisplayAlert("Ошибка", ex.Message, "ОК");
            }

        }

        //обработка нажатия кнопки удаления заметки
        private void DeleteNote(object sender, EventArgs e)
        {
            try
            {
                var note = (Note)BindingContext;
                //работа с бд
                using (ApplicationContext db = new ApplicationContext(dbPath))
                {
                    db.Notes.Remove(note);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                DisplayAlert("Ошибка", "Невозможно удалить пустую заметку!!!", "ОК");
            }
            this.Navigation.PopAsync();
        }

        private void AndroidSpecific()
        {
            App.Current.On<Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Unspecified);
            App.Current.On<Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            btnSave.On<Android>().SetUseDefaultPadding(true).SetUseDefaultShadow(true);
            btnDelete.On<Android>().SetUseDefaultPadding(true).SetUseDefaultShadow(true);
        }
	}
}