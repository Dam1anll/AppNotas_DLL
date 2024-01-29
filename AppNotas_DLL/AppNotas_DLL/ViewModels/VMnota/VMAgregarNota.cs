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
            var funcion = new NotaDatos();
            var datos = new NotaModelo();
            datos.Titulo = TxtTitulo;
            datos.Texto = TxtTexto;

            await funcion.AgregarNota(datos);
            await Volver();
            MessagingCenter.Send(this, "NotasActualizadas");
            
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
