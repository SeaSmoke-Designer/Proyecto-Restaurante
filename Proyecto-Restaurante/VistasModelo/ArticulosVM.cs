using CommunityToolkit.Mvvm.ComponentModel;
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
    class ArticulosVM : ObservableObject
    {
        private readonly ServicioAPIRestRestaurante servicioAPIRestRestaurante;

        private Producto nuevoProducto;
        public Producto NuevoProducto
        {
            get { return nuevoProducto; }
            set { SetProperty(ref nuevoProducto, value); }
        }

        private Producto productoSeleccionado;
        public Producto ProductoSeleccionado
        {
            get { return productoSeleccionado; }
            set { SetProperty(ref productoSeleccionado, value); }
        }

        private ObservableCollection<Categoria> listaCategorias;

        public ObservableCollection<Categoria> ListaCategorias
        {
            get { return listaCategorias; }
            set { SetProperty(ref listaCategorias, value); }
        }


        private Categoria categoriaSeleccionada;

        public Categoria CategoriaSeleccionada
        {
            get { return categoriaSeleccionada; }
            set { SetProperty(ref categoriaSeleccionada, value); }
        }

        private ObservableCollection<Producto> listaProductos;

        public ObservableCollection<Producto> ListaProductos
        {
            get { return listaProductos; }
            set { SetProperty(ref listaProductos, value); }
        }



        public ArticulosVM()
        {
            servicioAPIRestRestaurante = new ServicioAPIRestRestaurante();
            CargarCategorias();
            CargarProductos();
            
        }

        public void CargarCategorias()
        {
            ListaCategorias = servicioAPIRestRestaurante.GetCategorias();
        }

        public void CargarProductos()
        {
            ListaProductos = servicioAPIRestRestaurante.GetProductos();
        }
    }
}
