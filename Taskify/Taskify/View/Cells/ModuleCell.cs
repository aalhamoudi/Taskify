using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Taskify.ViewModel.Cells
{
    class ModuleCell : ViewCell
    {
        public ModuleCell()
        {
            // Views
            var cellWrapper = new StackLayout() { Padding = new Thickness(5), BackgroundColor = Color.FromHex("#e0e0e0") };
            var cellView = new StackLayout() { Orientation = StackOrientation.Horizontal };
            var moduleIcon = new Image() { HeightRequest = 60, WidthRequest = 60, VerticalOptions = LayoutOptions.Center, Source = ImageSource.FromResource("Taskify.Resources.Icons.module.png") };
            var moduleName = new Label() { VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.FillAndExpand, FontSize = Device.GetNamedSize(NamedSize.Large, this), TextColor = Color.Black };



            // Binding
            //projectIcon.SetBinding(Image.SourceProperty, "Icon");
            moduleName.SetBinding(Label.TextProperty, "Name");


            // Setup
            cellView.Children.Add(moduleIcon);
            cellView.Children.Add(moduleName);
            cellWrapper.Children.Add(cellView);
            View = cellWrapper;

        }
    }
}
