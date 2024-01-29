using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using AppNotas_DLL.Views;
using AppNotas_DLL.Models;
using AppNotas_DLL.Datos;
using AppNotas_DLL.Views.Nota;

namespace AppNotas_DLL.ViewModels.VMnota
{
    public class VMGestionNota : BaseViewModel
    {
        #region VARIABLES
        private string _txtTitulo;
        private string _txtTexto;
        public NotaModelo _nota { get; set; }
        #endregion
        #region CONTRUCTOR 
        public VMGestionNota(INavigation navigation, NotaModelo notaModelo) 
        {
            Navigation = navigation;
            _nota = notaModelo;
        }
        #endregion
        #region OBJETOS
        public string TxtTitulo 
        {
            get { return _nota.Titulo; }
            set { SetValue(ref _txtTitulo, value); }    
        }
        public string TxtTexto 
        {
            get { return _nota.Texto; }
            set { SetValue(ref _txtTexto, value); }
        }

        #endregion
        #region PROCESOS
        public async Task Editar() 
        {
            var funcion = new NotaDatos();
            var datos = new NotaModelo();
            datos.IdNota = _nota.IdNota;
            datos.Titulo = TxtTitulo;
            datos.Texto = TxtTexto;

            await funcion.EditarNota(datos);
            await Volver();
            MessagingCenter.Send(this, "NotasActualizadas");
        }

        public async Task Eliminar()
        {
            var funcion = new NotaDatos();
            await funcion.EliminarNota(_nota);
            await Volver();
            MessagingCenter.Send(this, "NotasActualizadas");
            
        }

        public async Task Volver() 
        {
            await Navigation.PopAsync();
        }
        #endregion
        #region COMANDOS
        public ICommand EditarCommand => new Command(async () => await Editar());
        public ICommand EliminarCommand => new Command(async () => await Eliminar());
        public ICommand VolverCommand => new Command(async ()=> await Volver());
        #endregion
    }
}
