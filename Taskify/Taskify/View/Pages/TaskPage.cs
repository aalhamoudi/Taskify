using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taskify.Model;
using Taskify.ViewModel.Views;
using Xamarin.Forms;

namespace Taskify.View.Pages
{
    class TaskPage : ContentPage
    {
        public Task task;
        public TaskPage(TasksPage page, Task task)
        {
            this.task = task;
            BackgroundColor = Color.Gray;

            Entry taskName = new Entry
            {
                Text = task.Name,
                PlaceholderColor = Constants.PlaceholderColor,
                TextColor = Color.White,
                Placeholder = "Task Name"
            };

            Entry description = new Entry
            {
                Text = task.Description,
                TextColor = Color.White,
                PlaceholderColor = Constants.PlaceholderColor,
                Placeholder = "Description",
                HeightRequest = 100
            };

            DatePicker dueDate = new DatePicker
            {
                Date = task.dueDate,
                Format  = "D"


            };

            Picker status = new Picker
            {
                Title = "Status"
            };

            status.Items.Add("To Do");
            status.Items.Add("Doing");
            status.Items.Add("Done");

            status.SelectedIndex = (int)task.TaskStatus;

            status.SelectedIndexChanged += (s, e) =>
            {
                if (status.SelectedIndex == -1)
                    task.TaskStatus = Status.Todo;
                else
                {
                    task.TaskStatus = (Status)status.SelectedIndex;
                }
            };

            Button save = new Button
            {
                Text = "Save",
                TextColor = Color.White,
               
            };

            save.Clicked += (s, e) =>
            {
                task.Name = taskName.Text;
                task.Description = description.Text;
                task.dueDate = dueDate.Date;
                App.db.AddTask(task);
                page.RefreshTasks();
                NavigationDrawer.NavContent?.PopAsync();
            };
            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 10,
                Padding = new Thickness(15),
                Children =
                {
                    taskName,
                    description,
                    dueDate,
                    status,
                    save
                }
            };
        }
    }
}
