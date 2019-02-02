using SQLite;
using System.IO;
using Xamarin.Forms;
using Taskify.Data;

[assembly: Dependency(typeof(Taskify.Droid.Services.Data.SQLite_Android))]

namespace Taskify.Droid.Services.Data
{
    
    public class SQLite_Android : ISQLite
    {
         public SQLite_Android()
         {

         }

        #region ISQLite implementation
        public SQLiteConnection GetConnection()
        {
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Local.db");

            var connection = new SQLiteConnection(path);
            return connection;
        }
        #endregion
    }
}