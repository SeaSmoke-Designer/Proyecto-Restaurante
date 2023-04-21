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
    class ArticulosVM : ObservableObject
    {
        private readonly ServicioAPIRestRestaurante servicioAPIRestRestaurante;
        public RelayCommand RefrescarListaProductosCommand { get; }


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
            RefrescarListaProductosCommand = new RelayCommand(RefrescarListaProductos);
            
        }

        public void CargarCategorias()
        {
            ListaCategorias = servicioAPIRestRestaurante.GetCategorias();
        }

        public void CargarProductos()
        {
            ListaProductos = servicioAPIRestRestaurante.GetProductos();
        }
        public void RefrescarListaProductos()
        {
            CargarProductos();
            CategoriaSeleccionada = null;
            
        }

        public void CargarProductosFiltrados()
        {
            ObservableCollection<Producto> listaAux = new ObservableCollection<Producto>();
            ObservableCollection<Producto> listaAux2 = servicioAPIRestRestaurante.GetProductos();
            if(CategoriaSeleccionada != null)
            {
                foreach (Producto item in listaAux2)
                {
                    if (item.IdCategoria.NombreCategoria == CategoriaSeleccionada.NombreCategoria)
                        listaAux.Add(item);
                }
                ListaProductos = listaAux;
            }
            
        }
    }
}
