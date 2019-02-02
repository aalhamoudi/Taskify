using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Taskify.View
{
    public interface TasksPage
    {
        ObservableCollection<Model.Task> tasks { get; set; }
        ListView TaskList { get; set; }
        void RefreshTasks();
    }
}
