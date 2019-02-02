using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Taskify.Model;
using Taskify.View.Pages;
using Taskify.ViewModel.Cells;
using Xamarin.Forms;

namespace Taskify.View
{
    public class Dashboard : NavigationDrawerItem, TasksPage
    {
        static int taskNum = 1;
        SearchBar searchBar;

        public Dashboard()
        {
            Title = "Dashboard";
            BackgroundColor = Constants.BackgroundColor;
            Padding = new Thickness(0, Device.OnPlatform(iOS: 30, Android: 10, WinPhone: 10), 0, 0);


          

            TaskList = new ListView
            {
                ItemsSource = tasks,
                ItemTemplate = new DataTemplate(typeof(TaskCell)),
                SeparatorVisibility = SeparatorVisibility.Default,
                RowHeight = 70
            };

            searchBar = new SearchBar()
            {
                Placeholder = "Search Tasks",
                PlaceholderColor = Color.Gray,
                TextColor = Color.Black,
                SearchCommand = new Command(() =>
                {
                    tasks = new ObservableCollection<Task>(new List<Model.Task>(App.db.GetTaskByName(searchBar.Text)));
                    TaskList.ItemsSource = tasks;

                })
            };
            RefreshTasks();

            TaskList.IsPullToRefreshEnabled = true;

            TaskList.ItemTapped += (s, e) => Navigation.PushAsync(new TaskPage(this, (Model.Task)((ListView)s).SelectedItem));

            TaskList.Refreshing += (s, e) => RefreshTasks();


            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = 15,

                Children = {
                    searchBar,
                    TaskList,
                    
                    }

            };

            this.Appearing += (s, e) => RefreshTasks();

        }

        public ListView TaskList { get; set; }

        public ObservableCollection<Task> tasks { get; set; }

        public void RefreshTasks()
        {
            var sortedTasks = new List<Model.Task>(App.db.GetStatusTasks(Status.Todo).Concat<Task>(App.db.GetStatusTasks(Status.Doing)));
            sortedTasks.Sort((x, y) => DateTime.Compare(x.dueDate, y.dueDate));
            tasks = new ObservableCollection<Model.Task>(sortedTasks);
            TaskList.ItemsSource = tasks;
            TaskList.IsRefreshing = false;

        }

}
}
