using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taskify.Model;
using Xamarin.Forms;

namespace Taskify.Data
{
    public class LocalDB
    {
		static object locker = new object();
        SQLiteConnection database;
        public LocalDB()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Project>();
            database.CreateTable<Model.Task>();


        }

        public IEnumerable<Project> GetProjects()
        {
            return (from i in database.Table<Project>() select i).ToList();
        }

        public void AddProject(Project project)
        {
			lock (locker){if(project.ID != 0){database.Update(project);}
			else{            database.Insert(project);}
			}
        }
		

        

        public int DeleteProject(int id)
        {
            DeleteProjectTasks(id);
           return database.Delete<Project>(id);
        }
        private void DeleteProjectTasks(int id)
        {
            List<Task> deleteTasks = new List<Task>();
            deleteTasks = (from i in database.Table<Model.Task>() select i).Where(i => i.P_ID == id).ToList();
            foreach (var task in deleteTasks)
            {
                database.Delete<Model.Task>(task.ID);
            }
            
        }

        public IEnumerable<Model.Task> GetTasks()
        {
            lock (locker)
            {
                return (from i in database.Table<Model.Task>() select i).ToList();

            }
        }
        public IEnumerable<Model.Task> GetStatusTasks(Status status)
        {
            lock (locker)
            {
                return (from i in database.Table<Model.Task>() select i).Where(i => i.TaskStatus == status).ToList();

            }
        }

        public IEnumerable<Model.Task> GetProjectTasks(int projectId)
        {
            lock (locker)
            {
                return (from i in database.Table<Model.Task>() select i).Where(i => i.P_ID == projectId).ToList();
            }
        }

        public void AddTask(Model.Task task)
        {
            lock (locker)
            {
                if (task.ID != 0) { database.Update(task); }
                else { database.Insert(task); }
            }
        }

        public Task GetTask(int id)
        {
            lock (locker)
            {
                return database.Get<Task>(id);
            }
            
        }

        public Project GetProject(int id)
        {
            lock (locker)
            {
                return database.Get<Project>(id);
            }

        }


        public int DeleteTask(int id)
        {
            return database.Delete<Model.Task>(id);
        }

        public IEnumerable<Task> GetTaskByName(string taskName)
        {
            return (from i in database.Table<Model.Task>() select i).Where(i => i.Name.Contains(taskName)).ToList<Task>();
        }
    }
}

