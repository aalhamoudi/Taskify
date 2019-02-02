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
    public class Taskbox : TabbedPage
    {
        public Taskbox()
        {
            Title = "Taskbox";

            ItemsSource = new TaskView[]
            {
                new TaskView { Title = "To Do", ViewStatus = Status.Todo },
                new TaskView { Title = "Doing", ViewStatus = Status.Doing },
                new TaskView { Title = "Done", ViewStatus = Status.Done }
            };

            

            ItemTemplate = new DataTemplate(() => new TaskViewPage());

            BackgroundColor = Constants.BackgroundColor;

        }


        class TaskView
        {
            public string Title { get; set; }

            public Status ViewStatus { get; set; }
        }
       

        public class TaskViewPage : ContentPage, TasksPage
        {

            public static readonly BindableProperty PageViewStatusProperty = BindableProperty.Create("PageViewSttus", typeof(Status), typeof(TaskViewPage), Status.Todo);
            public Status PageViewStatus { get { return (Status)GetValue(PageViewStatusProperty); } set { SetValue(PageViewStatusProperty, value); } }

            public ObservableCollection<Task> tasks { get; set; } = new ObservableCollection<Task>();
            public TaskViewPage()
            {
                this.SetBinding(ContentPage.TitleProperty, "Title");
                this.SetBinding(TaskViewPage.PageViewStatusProperty, "ViewStatus");
                
                BackgroundColor = Constants.BackgroundColor;

                TaskList = new ListView
                {
                    ItemsSource = tasks,
                    ItemTemplate = new DataTemplate(typeof(TaskCell)),
                    SeparatorVisibility = SeparatorVisibility.Default,
                    RowHeight = 70
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
                    TaskList
                    }

                };

                this.Appearing += (s, e) => RefreshTasks();


            }

            public ListView TaskList { get; set;}

           
            public void RefreshTasks()
            {
                tasks = new ObservableCollection<Model.Task>(App.db.GetStatusTasks(PageViewStatus));
                TaskList.ItemsSource = tasks;
                TaskList.IsRefreshing = false;

            }
        }

      

    }
}
