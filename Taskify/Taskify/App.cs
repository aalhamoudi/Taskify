using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Taskify.Data;
using Taskify.View;
using Xamarin.Forms;

namespace Taskify
{
    public class App : Application
    {
        public static LocalDB db = new LocalDB();
        
        public App()
        {

            //var nav = new NavigationDrawer();
            var nav = new NavigationDrawer();
            MainPage = nav;
            
        }

        protected override void OnStart()
        {
            //MainPage?.DisplayAlert("Start", "App Started", "OK");

        }

        protected override void OnSleep()
        {
            //MainPage?.DisplayAlert("Sleep", "App is Sleeping", "OK");

        }

        protected override void OnResume()
        {
            //MainPage?.DisplayAlert("Resume", "App Resumed", "OK");
        }
    }
}
