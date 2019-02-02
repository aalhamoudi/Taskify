using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using SQLite;

namespace Taskify.Model
{
    public class Project
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [JsonProperty(PropertyName = "name")]

        public string Name { get; set; }

        [JsonProperty(PropertyName = "complete")]
        public bool Complete { get; set; }

        //public ObservableCollection<Module> modules { get; set; } //= new ObservableCollection<Module>();

    }
}
