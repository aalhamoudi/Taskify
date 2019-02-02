using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskify.Model;
using Xamarin.Forms;

namespace Taskify.View.Pages
{
    class ProjectEditPage : ContentPage
    {

        public ProjectEditPage(Projects page, Project project)
        {
            BackgroundColor = Color.Gray;

            Entry projectName = new Entry
            {
                Text = project.Name,
                PlaceholderColor = Constants.PlaceholderColor,
                TextColor = Color.White,
                Placeholder = "Project Name"
            };


            Button save = new Button
            {
                Text = "Save",
                TextColor = Color.White,

            };

            save.Clicked += (s, e) =>
            {
                project.Name = projectName.Text;
                App.db.AddProject(project);
                page.RefreshProjects();
                NavigationDrawer.NavContent?.PopAsync();
            };
            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 10,
                Padding = new Thickness(15),
                Children =
                {
                    projectName,
                    save
                }
            };
        }
    }
}
