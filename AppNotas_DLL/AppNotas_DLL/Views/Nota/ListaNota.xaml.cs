using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppNotas_DLL.Models;
using AppNotas_DLL.ViewModels.VMnota;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppNotas_DLL.Views.Nota
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaNota : ContentPage
    {
        public ListaNota()
        {
            InitializeComponent();
            BindingContext = new VMListaNota(Navigation);
        }
    }
}