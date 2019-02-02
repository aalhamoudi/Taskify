using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;

namespace Taskify.Model
{
    public enum Status { Todo, Doing, Done };

    public class Task
    {


        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Status TaskStatus { get; set; }
        public int P_ID { get; set; }
        public DateTime dueDate { get; set; }
    }
}
