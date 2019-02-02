using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taskify.Model;
using Xamarin.Forms;

namespace Taskify.Data
{
    class TaskDB
    {
		static object locker = new object();
        SQLiteConnection database;
        public TaskDB()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Model.Task>();

        }

        public IEnumerable<Model.Task> GetTasks(int id)
        {
            lock (locker)
			{
                return database.Query<Model.Task>("SELECT * FROM [Task] WHERE [P_ID] = [id]");
			}
        }
		public IEnumerable<Model.Task> GetStatusTasks(Status status)
		{
			lock (locker)
			{
			return database.Query<Model.Task>("SELECT * FROM [Task] WHERE [TaskStatus] = [status]");
			}
		}

        public IEnumerable<Model.Task> GetProjectTasks(int projectId)
        {
            lock (locker)
            {
                return database.Query<Model.Task>("SELECT * FROM [Task] WHERE [P_ID] = [projectId]");
            }
        }

        public void AddTask(Model.Task task)
        {
			lock (locker){if(task.ID != 0){database.Update(task);}
			else{            database.Insert(task);}
			}
        }
		

        

        public int DeleteTask(int id)
        {
            return database.Delete<Model.Task>(id);
        }
    }
}
