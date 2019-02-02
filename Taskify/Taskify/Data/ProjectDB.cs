using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taskify.Model;
using Xamarin.Forms;

namespace Taskify.Data
{
    class ProjectDB
    {
        static object locker = new object();
        SQLiteConnection database;
        public ProjectDB()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Project>();


        }

        public IEnumerable<Project> GetProjects()
        {
            return (from i in database.Table<Project>() select i).ToList();
        }

        public void AddProject(Project project)
        {
            lock (locker)
            {
                if (project.ID != 0) { database.Update(project); }
                else { database.Insert(project); }
            }
        }




        public int DeleteProject(int id)
        {
            return database.Delete<Project>(id);
        }


    }
}

