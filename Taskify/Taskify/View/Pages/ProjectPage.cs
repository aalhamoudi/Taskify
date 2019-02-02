using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskify.Data;
using Taskify.Model;
using Taskify.View;
using Taskify.View.Pages;
using Taskify.ViewModel.Cells;
using Xamarin.Forms;

namespace Taskify.ViewModel.Views
{
    class ProjectPage : ContentPage, TasksPage
    {
        Project project;
        static int taskNum = 1;
        public ObservableCollection<Model.Task> tasks { get; set; } = new ObservableCollection<Model.Task>();
        public ListView TaskList { get; set; }
        public ProjectPage(Project project)
        {
            this.project = project;
            BackgroundColor = Constants.BackgroundColor;

            TaskList = new ListView
            {
                ItemsSource = tasks,
                ItemTemplate = new DataTemplate(typeof(TaskCell)),
                SeparatorVisibility = SeparatorVisibility.Default,
                RowHeight = 70
            };

            RefreshTasks();

            TaskList.ItemTapped += (s, e) => Navigation.PushAsync(new TaskPage(this, (Model.Task)((ListView)s).SelectedItem));
            TaskList.IsPullToRefreshEnabled = true;

            Button button = new Button
            {
                Text = "Add Task",

            };
            button.Clicked += (s, e) =>
            {
                var task = new Model.Task { Name = $"Task {taskNum++}", P_ID = project.ID, TaskStatus = Status.Todo, dueDate = DateTime.Now };
                tasks.Add(task);
                App.db.AddTask(task);
                Navigation.PushAsync(new TaskPage(this, task));

            };

            TaskList.Refreshing += (s, e) => RefreshTasks();


            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = 15,

                Children = {
                    TaskList,
                    button
                    }

            };

            this.Appearing += (s, e) => RefreshTasks();

        }

        public void RefreshTasks()
        {
            tasks = new ObservableCollection<Model.Task>(App.db.GetProjectTasks(project.ID));
            TaskList.ItemsSource = tasks;
            TaskList.IsRefreshing = false;

        }
    }
    
}
