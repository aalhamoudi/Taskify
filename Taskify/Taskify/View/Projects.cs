using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Taskify.Data;
using Taskify.Model;
using Taskify.View.Pages;
using Taskify.ViewModel.Cells;
using Taskify.ViewModel.Views;
using Xamarin.Forms;

namespace Taskify.View
{
    public class Projects : ContentPage
    {
        public static ObservableCollection<Project> projects { get; set; } = new ObservableCollection<Project>();
        public static int projectNum = 1;
        public ListView ProjectList { get; set; }
        public Projects()
        {
            
            projects = new ObservableCollection<Project>(App.db.GetProjects());

            ProjectList = new ListView
            {
                ItemsSource = projects,
                ItemTemplate = new DataTemplate(typeof(ProjectCell)),
                SeparatorVisibility = SeparatorVisibility.Default,
                RowHeight = 70
            };

            ProjectList.ItemTapped += (s, e) =>
            {
                NavigationDrawer.Selected = typeof(ProjectPage);
                Navigation.PushAsync(new ProjectPage((Project)((ListView)s).SelectedItem));
                ProjectList.IsPullToRefreshEnabled = true;
                
            };

            ProjectList.Refreshing += (s, e) => RefreshProjects();

            Button button = new Button
            {
                Text = "Add Project",

            };
            button.Clicked += (s, e) =>
            {
                var project = new Project { Name = $"Project {projectNum++}" };
                projects.Add(project);
                App.db.AddProject(project);

                Navigation.PushAsync(new ProjectEditPage(this, project));

            };
            button.BorderRadius = 20;

          

            var layout = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = 15,
                BackgroundColor = Color.FromHex("#bdbdbd"),
                
                Children = {
                    ProjectList,
                    button
                    }
            
            };


            this.Appearing += (s, e) => RefreshProjects();

            Content = layout;
        }

        public void RefreshProjects()
        {
            projects = new ObservableCollection<Project>(App.db.GetProjects());
            ProjectList.ItemsSource = projects;
            ProjectList.IsRefreshing = false;
        }

    }
}
