using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Taskify.View
{
    public class Agenda : CarouselPage
    {
        public Agenda()
        {
            BackgroundColor = Constants.BackgroundColor;

            var view = new ContentView
            {

                Content = new StackLayout()
                {
                    Padding = new Thickness(0, Device.OnPlatform(iOS: 25, Android: 5, WinPhone: 5), 0, 0),
                    Children =
                        {
                            new Label
                            {
                                Text = DateTime.Today.Date.ToString("D"),
                                TextColor = Color.Black,
                                FontSize = Device.GetNamedSize(NamedSize.Large, this),
                                HorizontalOptions = LayoutOptions.CenterAndExpand,

                            }
                        }
                }
            };

            var panGesture = new PanGestureRecognizer();

            panGesture.PanUpdated += (s, e) =>
            {
                if (e.TotalX > 0)
                {

                }

                else if (e.TotalX < 0)
                {

                }
            };

            view.GestureRecognizers.Add(panGesture);

            Children.Add(new ContentPage() {


                Content = view

                });
           
        }
    }
}
