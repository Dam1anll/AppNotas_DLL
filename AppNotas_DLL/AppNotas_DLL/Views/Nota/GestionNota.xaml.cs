﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppNotas_DLL.ViewModels;
using AppNotas_DLL.Views;
using AppNotas_DLL.Models;
using AppNotas_DLL.Datos;
using AppNotas_DLL.ViewModels.VMnota;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppNotas_DLL.Views.Nota
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GestionNota : ContentPage
    {
        public GestionNota(NotaModelo notaModelo)
        {
            InitializeComponent();
            BindingContext = new VMGestionNota(Navigation, notaModelo);
            var viewModel = new VMGestionNota(Navigation, notaModelo);
            viewModel.NotaEditadaCorrectamente += (sender, mensaje) =>
            {
                MostrarMensaje(mensaje);
            };
            viewModel.NotaBorradaCorrectamente += (sender, mensaje) =>
            {
                MostrarMensaje(mensaje);
            };
            BindingContext = viewModel;
        }
        private async void MostrarMensaje(string mensaje)
        {
            await DisplayAlert("Mensaje", mensaje, "Aceptar");
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