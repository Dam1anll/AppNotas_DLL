using System;
using System.Collections.Generic;
using System.Text;
using AppNotas_DLL.Models;
using AppNotas_DLL.Conexion;
using Firebase.Database.Query;
using Firebase.Database;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Essentials;

namespace AppNotas_DLL.Datos
{
    public class NotaDatos
    {
        public async Task AgregarNota(NotaModelo datos) 
        {
            await Cconexion.firebase
                .Child("Notas")
                .PostAsync(new NotaModelo()
                {
                    Titulo = datos.Titulo,
                    Texto = datos.Texto
                });
        }

        public async Task EliminarNota(NotaModelo notaModelo) 
        {
            string idNota = notaModelo.IdNota;
            await Cconexion.firebase.Child("Notas").Child(idNota).DeleteAsync();
        }

        public async Task EditarNota(NotaModelo datos) 
        {
            await Cconexion.firebase.Child("Notas").Child(datos.IdNota).PutAsync(new NotaModelo
            {
                Titulo = datos.Titulo,
                Texto = datos.Texto
            });
        }
        public async Task<ObservableCollection<NotaModelo>> MostrarNota()
        {
            var notas = await Cconexion.firebase.Child("Notas").OnceAsync<NotaModelo>();

            ObservableCollection<NotaModelo> listaObservable = new ObservableCollection<NotaModelo>(
                notas.Select(item => new NotaModelo
                {
                    IdNota = item.Key,
                    Titulo = item.Object.Titulo,
                    Texto = item.Object.Texto
                })
            );

            return listaObservable;
        }
        public static async Task<List<NotaModelo>> ObtenerNotas()
        {
            var notas = await Cconexion.firebase
                .Child("Notas")
                .OnceAsync<NotaModelo>();

            return notas.Select(item => new NotaModelo
            {
                IdNota = item.Key,
                Titulo = item.Object.Titulo,
                Texto = item.Object.Texto
            }).ToList();
        }

        /*public async Task<List<NotaModelo>> MostrarNota() 
        {
            return (await Cconexion.firebase.Child("Notas").OnceAsync<NotaModelo>()).Select(item => new NotaModelo
            {
                IdNota = item.Key,
                Titulo = item.Object.Titulo,
                Texto = item.Object.Texto
            }).ToList();
        }*/
    }
}
