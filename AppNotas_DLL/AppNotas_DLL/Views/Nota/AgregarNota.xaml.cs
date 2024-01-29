using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppNotas_DLL.ViewModels.VMnota;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppNotas_DLL.Views.Nota
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarNota : ContentPage
    {
        public AgregarNota()
        {
            InitializeComponent();
            BindingContext = new VMAgregarNota(Navigation);
        }
        private void MaxText(object sender, TextChangedEventArgs e)
        {
            var editor = (Editor)sender;
            var maxCharactersPerLine = 20; 
            var lines = editor.Text.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Length > maxCharactersPerLine)
                {
                    lines[i] = lines[i].Substring(0, maxCharactersPerLine);
                }
            }
            editor.Text = string.Join("\n", lines);
        }
    }
}