using System;
using System.IO;
using Android.OS;
using Notes.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidDbPath))]
namespace Notes.Droid
{
    public class AndroidDbPath : IPath
    {
        public string GetDatabasePath(string filename)
        {
            return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), filename);
        }
    }
}