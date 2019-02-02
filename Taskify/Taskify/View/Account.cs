using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Taskify.View
{
    public class Account : ContentPage
    {
        public Account()
        {
            BackgroundColor = Constants.BackgroundColor;

            Content = new StackLayout
            {
                Children = {
                    new Label { Text = "Hello ContentPage" }
                }
            };
        }
    }
}
