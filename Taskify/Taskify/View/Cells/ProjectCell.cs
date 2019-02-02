using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taskify.View;
using Taskify.View.Pages;
using Xamarin.Forms;

namespace Taskify.ViewModel.Cells
{
    class ProjectCell : ViewCell
    {
        public static readonly BindableProperty ProjectIDProperty = BindableProperty.Create("ProjectID", typeof(int), typeof(ProjectCell), -1);
        public int ProjectID
        {
            get
            {
                return (int)GetValue(ProjectIDProperty);
            }
            set
            {
                SetValue(ProjectIDProperty, value);
            }
        }
        public ProjectCell()
        {
            // Views
            var cellWrapper = new StackLayout() { Padding = new Thickness(5), BackgroundColor = Color.FromHex("#e0e0e0") };
            var cellView = new StackLayout() { Orientation = StackOrientation.Horizontal };
            var projectIcon = new Image() { HeightRequest = 60, WidthRequest = 60, VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromResource("Taskify.Resources.Icons.project.png") };
            var projectName = new Label() { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand, FontSize = Device.GetNamedSize(NamedSize.Large, this), TextColor = Color.Black };

            this.SetBinding(ProjectCell.ProjectIDProperty, "ID");

            var editAction = new MenuItem { Text = "Edit" };
            editAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            editAction.Clicked += (s, e) => {
                NavigationDrawer.NavContent.PushAsync(new ProjectEditPage(NavigationDrawer.Projects, App.db.GetProject(ProjectID)));
            };

            var deleteAction = new MenuItem { Text = "Delete", IsDestructive = true };
            deleteAction.SetBinding(MenuItem.CommandParameterProperty, new Binding("."));
            deleteAction.Clicked += (s, e) => { App.db.DeleteProject(ProjectID); NavigationDrawer.Projects.ProjectList.BeginRefresh(); };

            ContextActions.Add(editAction);
            ContextActions.Add(deleteAction);

            // Binding
            //projectIcon.SetBinding(Image.SourceProperty, "Icon");
            projectName.SetBinding(Label.TextProperty, "Name");


            // Setup
            cellView.Children.Add(projectIcon);
            cellView.Children.Add(projectName);
            cellWrapper.Children.Add(cellView);
            View = cellWrapper;

        }
    }
}
