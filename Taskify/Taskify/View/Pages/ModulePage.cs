using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskify.Model;
using Taskify.ViewModel.Cells;
using Xamarin.Forms;

namespace Taskify.ViewModel.Views
{
    class ModulePage : ContentPage
    {
        Module module;
        static int taskNum = 1;

        public ModulePage(Module module)
        {
            this.module = module;
            BackgroundColor = Constants.BackgroundColor;

            ListView taskList = new ListView
            {
                //ItemsSource = module.tasks,
                ItemTemplate = new DataTemplate(typeof(TaskCell)),
                SeparatorVisibility = SeparatorVisibility.Default,
                RowHeight = 70
            };
            taskList.ItemTapped += (s, e) => DisplayAlert("Item", "Show Item", "Cancel");
            taskList.IsPullToRefreshEnabled = true;
            taskList.Refreshing += (s, e) => { DisplayAlert("Item", "Show Item", "Cancel"); taskList.EndRefresh(); };

            Button button = new Button
            {
                Text = "Add Task",

            };
            //button.Clicked += (s, e) => module.tasks.Add(new Model.Task { Name = $"Task {taskNum++}" });


            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = 15,

                Children = {
                    taskList,
                    button
                    }

            };
        }
    }
    
}
