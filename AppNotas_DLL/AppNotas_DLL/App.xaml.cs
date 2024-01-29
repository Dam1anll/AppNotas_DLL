using AppNotas_DLL.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppNotas_DLL.Views.Nota;

namespace AppNotas_DLL
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new ListaNota());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
