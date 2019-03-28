using Xamarin.Forms;
using System.IO;
using Windows.Storage;
using Notes.UWP;

[assembly: Dependency(typeof(UwpDbPath))]
namespace Notes.UWP
{
    public class UwpDbPath : IPath
    {
        public string GetDatabasePath(string sqliteFilename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, sqliteFilename);
        }
    }
}
