using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Taskify.View
{
    public class NavigationDrawer : MasterDetailPage
    {
        public NavigationDrawerMenu NavDrawer { get; set; }
        public static NavigationPage NavContent { get; set; }

        public static Dashboard Dashboard { get; set; } = new Dashboard();
        public static Taskbox Taskbox { get; set; } = new Taskbox();
        public static Projects Projects { get; set; } = new Projects();

        public static Type Selected { get; set; } = typeof(Dashboard);
        public NavigationDrawer()
        {
            Master = NavDrawer =  new NavigationDrawerMenu();
            Detail = NavContent = new NavigationPage(Dashboard);


            NavDrawer.ListView.ItemTapped += (s, e) =>
            {
                this.IsPresented = false;
                ListView list = s as ListView;
                NavigationDrawerItem item = list.SelectedItem as NavigationDrawerItem;
                Type type = item.TargetType;
            
                if (type.Equals(Selected)) return;

                NavContent.PopAsync();
                if (type.Equals(typeof(Dashboard)))
                    NavContent.PushAsync(Dashboard);
                else if (type.Equals(typeof(Taskbox)))
                    NavContent.PushAsync(Taskbox);
                else if (type.Equals(typeof(Projects)))
                    NavContent.PushAsync(Projects);
                else
                    NavContent.PushAsync(Dashboard);
                Selected = type;
            };

            BackgroundColor = Constants.BackgroundColor;



        }
    }


    public class NavigationDrawerMenu : ContentPage
    {
        public class NavigationDrawerItemGroup : List<NavigationDrawerItem>
        {
            public string Heading { get; set; }

            public NavigationDrawerItemGroup(string heading)
            {
                Heading = heading;
            }
        }

        public ListView ListView { get { return drawerItemsList; } }

        ListView drawerItemsList;
        List<NavigationDrawerItemGroup> drawerItems = new List<NavigationDrawerItemGroup>();


        public NavigationDrawerMenu()
        {

            NavigationDrawerItemGroup views = new NavigationDrawerItemGroup("Views");
            NavigationDrawerItemGroup preferences = new NavigationDrawerItemGroup("Preferences");


            views.Add(new Dashboard
            {
                MenuHeading = "Dashboard",
                IconSource = ImageSource.FromResource("Taskify.Resources.Icons.dashboard.png"),
                TargetType = typeof(Dashboard)
            });

            views.Add(new NavigationDrawerItem
            {
                MenuHeading = "Taskbox",
                IconSource = ImageSource.FromResource("Taskify.Resources.Icons.taskbox.png"),
                TargetType = typeof(Taskbox)
            });

            //views.Add(new NavigationDrawerItem
            //{
            //    MenuHeading = "Agenda",
            //    IconSource = ImageSource.FromResource("Taskify.Resources.Icons.agenda.png"),
            //    TargetType = typeof(Agenda)
            //});

            views.Add(new NavigationDrawerItem
            {
                MenuHeading = "Projects",
                IconSource = ImageSource.FromResource("Taskify.Resources.Icons.project.png"),
                TargetType = typeof(Projects)
            });


            preferences.Add(new NavigationDrawerItem
            {
                MenuHeading = "Account",
                IconSource = ImageSource.FromResource("Taskify.Resources.Icons.account.png"),
                TargetType = typeof(Account)
            });

            preferences.Add(new NavigationDrawerItem
            {
                MenuHeading = "Settings",
                IconSource = ImageSource.FromResource("Taskify.Resources.Icons.settings.png"),
                TargetType = typeof(Settings)
            });

            drawerItems.Add(views);
            //drawerItems.Add(preferences);

            drawerItemsList = new ListView
            {

                ItemsSource = drawerItems,
                ItemTemplate = new DataTemplate(() => {
                    var icon = new Image()
                    {
                        HeightRequest = 48,
                        WidthRequest = 48
                    };

                    var label = new Label()
                    {
                        TextColor = Color.Black,
                        VerticalTextAlignment = TextAlignment.Center,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                       
                    };

                    icon.SetBinding(Image.SourceProperty, "IconSource");
                    label.SetBinding(Label.TextProperty, "MenuHeading");

                    var cell = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        Padding = new Thickness(8, 5, 0, 5),
                        Children =
                        {
                            icon,
                            label
                        }
                    };

                    var view = new ViewCell
                    {
                        View = cell
                    };

                    return view;

                }),
                VerticalOptions = LayoutOptions.FillAndExpand,
                SeparatorVisibility = SeparatorVisibility.None,
                RowHeight = 70,
                BackgroundColor = Color.Transparent,
                IsGroupingEnabled = true,
                GroupShortNameBinding = new Binding("Heading"),
                HasUnevenRows = true,
                GroupHeaderTemplate = new DataTemplate(() => {

                    var title = new Label
                    {
                        TextColor = Color.Black,
                        FontSize = Device.GetNamedSize(NamedSize.Medium, this)
                    };

                    title.SetBinding(Label.TextProperty, "Heading");


                    var cell = new StackLayout
                    {
                        Padding = new Thickness(5, 25, 0, 5),
                        VerticalOptions = LayoutOptions.Center,

                        Children =
                        {
                            title
                        }
                    };


                    var header = new ViewCell();
                    header.View = cell;
                    return header;

                })

            };

          
            Title = "Views";
            //Icon = "hamburger.png";

            Padding = new Thickness(0, Device.OnPlatform(20, 0, 0) , 0, 0);
            BackgroundColor = Constants.BackgroundColor;
            
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    drawerItemsList
                }
            };
        }

      
    }

    public class NavigationDrawerItem : ContentPage
    {
        public string MenuHeading { get; set; }
        public ImageSource IconSource { get; set; }
        public Type TargetType { get; set; }

        public NavigationDrawerItem()
        {
            BackgroundColor = Color.FromHex("#e0e0e0");
            
            Content = new StackLayout
            {

            };
        }
    }

    class NavigationDrawerItemCell : ViewCell
    {
        public NavigationDrawerItemCell()
        {
            var cellWrapper = new StackLayout
            {
                Children =
                {

                }
            };
        }
    }
}
