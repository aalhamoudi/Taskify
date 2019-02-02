using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using SQLite;

namespace Taskify.Model
{
    public class Module
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }

        //public ObservableCollection<Model.Task> tasks = new ObservableCollection<Model.Task>();
    }
}
