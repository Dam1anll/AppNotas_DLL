using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using AppNotas_DLL.ViewModels;

namespace AppNotas_DLL.Models
{
    public class NotaModelo : BindableBase
    {
        public string IdNota { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }

        private bool _isSeleccionada;
        public bool IsSeleccionada
        {
            get { return _isSeleccionada; }
            set { SetProperty(ref _isSeleccionada, value); }
        }
    }
}
