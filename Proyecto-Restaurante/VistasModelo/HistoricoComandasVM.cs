using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Proyecto_Restaurante.Modelos;
using Proyecto_Restaurante.Servicios;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Proyecto_Restaurante.VistasModelo
{
    class HistoricoComandasVM : ObservableObject
    {
        public RelayCommand EliminarComandaCommand { get; }
        public RelayCommand VerComandaCommand { get; }

        private readonly ServicioAPIRestRestaurante servicioAPIRestRestaurante;
        private readonly ServicioDialogo servicioDialogo;

        private ObservableCollection<Comanda> listaComandas;

        public ObservableCollection<Comanda> ListaComandas
        {
            get { return listaComandas; }
            set { SetProperty(ref listaComandas, value); }
        }


        private Comanda comandaSeleccionada;

        public Comanda ComandaSeleccionada
        {
            get { return comandaSeleccionada; }
            set { SetProperty(ref comandaSeleccionada, value); }
        }


        public HistoricoComandasVM()
        {
            servicioAPIRestRestaurante = new ServicioAPIRestRestaurante();
            servicioDialogo = new ServicioDialogo();
            CargarComandas();
            EliminarComandaCommand = new RelayCommand(EliminarComanda);
        }

        public void CargarComandas()
        {
            ListaComandas = servicioAPIRestRestaurante.GetComandas();
        }

        public void EliminarComanda()
        {
            MessageBoxResult result = MessageBox.Show("¿Estas seguro de eliminar esta comanda?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                if (ComandaSeleccionada != null)
                {
                    IRestResponse response = servicioAPIRestRestaurante.DeleteProducto(ComandaSeleccionada.IdComanda);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ListaComandas.Remove(ComandaSeleccionada);
                        servicioDialogo.MostrarMensajeInformacion("Comanda eliminada con exito!", "Comanda Eliminada");
                    }
                    else
                    {
                        servicioDialogo.MostrarMensajeError(response.Content, "Error al borrar la comanda");
                    }
                }
            }
        }

    }
}
