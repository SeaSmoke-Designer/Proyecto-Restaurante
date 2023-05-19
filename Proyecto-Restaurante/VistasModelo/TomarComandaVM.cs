using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Proyecto_Restaurante.Modelos;
using Proyecto_Restaurante.Servicios;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.VistasModelo
{
    class TomarComandaVM : ObservableObject
    {
        private readonly ServicioAPIRestRestaurante servicioAPIRestRestaurante;
        private readonly ServicioDialogo servicioDialogo;

        public RelayCommand BorrarLineaCommand { get; }
        public RelayCommand BorrarComandaCommand { get; }
        public RelayCommand PasarAMesaCommand { get; }
        public RelayCommand CobrarComandaCommand { get; }

        private ObservableCollection<Producto> listaProductos;

        public ObservableCollection<Producto> ListaProductos
        {
            get { return listaProductos; }
            set { SetProperty(ref listaProductos, value); }
        }

        private ObservableCollection<Categoria> listaCategorias;

        public ObservableCollection<Categoria> ListaCategorias
        {
            get { return listaCategorias; }
            set { SetProperty(ref listaCategorias, value); }
        }

        private ObservableCollection<DetalleComanda> detallesComandaProductos;

        public ObservableCollection<DetalleComanda> DetallesComandaProductos
        {
            get { return detallesComandaProductos; }
            set { SetProperty(ref detallesComandaProductos, value); }
        }

        public TomarComandaVM()
        {
            servicioAPIRestRestaurante = new ServicioAPIRestRestaurante();
            servicioDialogo = new ServicioDialogo();
        }
    }
}
