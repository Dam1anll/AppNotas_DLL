using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using AppNotas_DLL.Models;
using AppNotas_DLL.ViewModels.VMnota;
using AppNotas_DLL.Datos;
using System.Diagnostics.Contracts;

namespace AppNotas_DLL.ViewModels.VMnota
{
    public class VMAgregarNota : BaseViewModel
    {
        #region VARIABLES
        private string _txtTitulo;
        private string _txtTexto;
        public event EventHandler<string> NotaAgregadaCorrectamente;
        #endregion
        #region CONTRUCTOR
        public VMAgregarNota(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion
        #region OBJETOS
        public string TxtTitulo 
        {
            get { return _txtTitulo; }
            set { SetValue(ref _txtTitulo, value); }
        }

        public string TxtTexto 
        {
            get { return _txtTexto; }
            set { SetValue(ref _txtTexto, value); } 
        }

        #endregion
        #region PROCESOS
        public async Task Agregar()
        {
            if (string.IsNullOrEmpty(TxtTitulo) || string.IsNullOrEmpty(TxtTexto))
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "Ingresa el título y el texto de la nota.", "OK");
                return;
            }

            var funcion = new NotaDatos();
            var datos = new NotaModelo();
            datos.Titulo = TxtTitulo;
            datos.Texto = TxtTexto;

            await funcion.AgregarNota(datos);
            await Volver();
            NotaAgregadaCorrectamente?.Invoke(this, "Nota Agregada correctamente");
        }
        public async Task Volver() 
        {
            await Navigation.PopAsync();
        }
        #endregion
        #region COMANDOS
        public ICommand AgregarCommand => new Command(async () => await Agregar());

        public ICommand VolverCommand => new Command(async () => await Volver()); 
        #endregion
    }
}
