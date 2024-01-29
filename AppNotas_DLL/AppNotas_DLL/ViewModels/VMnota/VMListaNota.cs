using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using AppNotas_DLL.Models;
using System.Reactive.Linq;
using AppNotas_DLL.Views.Nota;
using AppNotas_DLL.Datos;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Linq;

namespace AppNotas_DLL.ViewModels.VMnota
{
    public class VMListaNota : BaseViewModel
    {
        #region VARIABLES
        private ObservableCollection<NotaModelo> _listaNota;
        private ObservableCollection<NotaModelo> _notasSeleccionadas;
        public event EventHandler<string> NotasBorradasCorrectamente;
        #endregion
        #region CONTRUCTOR 
        public VMListaNota(INavigation navigation) 
        {
            Navigation = navigation;
            MostrarNota();
            MessagingCenter.Subscribe<VMGestionNota>(this, "NotasActualizadas", (sender) =>
            {
                MostrarNota();
            });
            MessagingCenter.Subscribe<VMAgregarNota>(this, "NotasActualizadas", (sender) =>
            {
                MostrarNota();
            });
        }
        #endregion
        #region OBJETOS
        public ObservableCollection<NotaModelo> ListaNota 
        {
            get { return _listaNota; }
            set { SetValue(ref _listaNota, value); }
        }
        public ObservableCollection<NotaModelo> NotasSeleccionadas
        {
            get
            {
                if (_notasSeleccionadas == null)
                {
                    _notasSeleccionadas = new ObservableCollection<NotaModelo>();
                }
                return _notasSeleccionadas;
            }
            set { SetValue(ref _notasSeleccionadas, value); }
        }
        #endregion
        #region PROCESOS
        public async Task MostrarNota()
        {
            var funcion = new NotaDatos();
            ListaNota = await funcion.MostrarNota();
        }
        private async Task SeleccionarNota(NotaModelo nota)
        {
            if (NotasSeleccionadas.Contains(nota))
            {
                NotasSeleccionadas.Remove(nota);
            }
            else
            {
                NotasSeleccionadas.Add(nota);
            }

            nota.IsSeleccionada = !nota.IsSeleccionada;

            await ObtenerIdsNotasSeleccionadas();
        }
        private async Task ObtenerIdsNotasSeleccionadas()
        {
            var idsNotasSeleccionadas = NotasSeleccionadas.Select(nota => nota.IdNota).ToList();
        }

        private async Task EliminarNotasSeleccionadas()
        {
            if (NotasSeleccionadas.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Aviso", "Selecciona al menos una nota para eliminar.", "OK");
                return;
            }

            var funcion = new NotaDatos();

            foreach (var nota in NotasSeleccionadas)
            {
                await funcion.EliminarNota(nota);
            }

            await MostrarNota();
            NotasSeleccionadas.Clear();
            NotasBorradasCorrectamente?.Invoke(this, "Nota(s) borrada(s) correctamente");
        }
        public async Task IraAgregar() 
        {
            await Navigation.PushAsync(new AgregarNota());
        }
        public async Task IraGestion(NotaModelo notaModelo) 
        {
            await Navigation.PushAsync(new GestionNota(notaModelo));
        }
        #endregion
        #region COMANDOS
        public ICommand IraAgregarCommand => new Command(async () => await IraAgregar());
        public ICommand IraGestionCommand => new Command<NotaModelo>(async (vista) => await IraGestion(vista));
        public ICommand SeleccionarNotaCommand => new Command<NotaModelo>(async (nota) => await SeleccionarNota(nota));
        public ICommand EliminarNotasSeleccionadasCommand => new Command(async () => await EliminarNotasSeleccionadas());
        #endregion
    }
}
