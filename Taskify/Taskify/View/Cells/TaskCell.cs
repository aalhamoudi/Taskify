using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Taskify.Model;
using Taskify.View;
using Taskify.ViewModel.Views;
using Xamarin.Forms;

namespace Taskify.ViewModel.Cells
{
    class TaskCell : ViewCell
    {
        public static readonly BindableProperty TaskIDProperty = BindableProperty.Create("TaskID", typeof(int), typeof(TaskCell), -1);
        public static readonly BindableProperty TaskStatusProperty = BindableProperty.Create("TaskID", typeof(Status), typeof(TaskCell), Status.Todo);

        public int TaskID
        {
            get
            {
                return (int)GetValue(TaskIDProperty);
            }
            set
            {
                SetValue(TaskIDProperty, value);
            }
        }

        public Status TaskStatus { get { return (Status)GetValue(TaskStatusProperty); } set { SetValue(TaskStatusProperty, value); } }

        public Task Task { get; set; }
        public TaskCell()
        {


            // Views
            var cellWrapper = new StackLayout() { Padding = new Thickness(5), BackgroundColor = Color.FromHex("#e0e0e0") };
            var cellView = new StackLayout() { Orientation = StackOrientation.Horizontal };
            var taskIcon = new Image() { HeightRequest = 60, WidthRequest = 60, VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromResource("Taskify.Resources.Icons.task.png") };
            var taskName = new Label() { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand, FontSize = Device.GetNamedSize(NamedSize.Large, this), TextColor = Color.Black };
            var taskDueDate = new Label() { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand, FontSize = Device.GetNamedSize(NamedSize.Medium, this), TextColor = Color.Red };

            this.SetBinding(TaskCell.TaskIDProperty, "ID");
            this.SetBinding(TaskCell.TaskStatusProperty, "TaskStatus");



            var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true };
            deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            deleteAction.Clicked += (s, e) =>
            {
                App.db.DeleteTask(TaskID);
                RefreshList();
            };


            ContextActions.Add(deleteAction);

            // Binding
            //projectIcon.SetBinding(Image.SourceProperty, "Icon");
            taskName.SetBinding(Label.TextProperty, "Name");
            taskDueDate.SetBinding(Label.TextProperty, "dueDate", converter: new DateConverter());


            var textLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    taskName,
                    taskDueDate
                }
            };

            // Setup
            cellView.Children.Add(taskIcon);
            cellView.Children.Add(textLayout);
            cellWrapper.Children.Add(cellView);
            View = cellWrapper;


            var swipe = new PanGestureRecognizer();
            swipe.PanUpdated += (s, e) =>
            {
                if (e.TotalX > 0)
                {
                    Task = App.db.GetTask(TaskID);

                    if (Task != null)
                    {
                        if ((int)Task.TaskStatus < 2)
                        {
                            Task.TaskStatus = (Status)(((int)Task.TaskStatus + 1) % 3);
                            App.db.AddTask(Task);

                            RefreshList();
                        }
                    }
                }
                else if (e.TotalX < 0)
                {
                    Task = App.db.GetTask(TaskID);
                    if (Task != null)
                    {
                        if ((int)Task.TaskStatus > 0)
                        {
                            Task.TaskStatus = (Status)(((int)Task.TaskStatus - 1) % 3);

                            App.db.AddTask(Task);
                            RefreshList();
                        }
                    }

                }
            };

            if (NavigationDrawer.Selected.Equals(typeof(Taskbox)))
                cellWrapper.GestureRecognizers.Add(swipe);



        }

        private void RefreshList()
        {
            (Parent as ListView).BeginRefresh();
        }

        class DateConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is DateTime)
                {
                    return ((DateTime)value).ToString("D");
                }
                else return value;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}
