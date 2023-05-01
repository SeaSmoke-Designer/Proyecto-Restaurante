using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Proyecto_Restaurante.Mensajes;
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
    class ArticulosVM : ObservableObject
    {
        private readonly ServicioNavegacion servicioNavegacion;
        private readonly ServicioAPIRestRestaurante servicioAPIRestRestaurante;
        private readonly ServicioDialogo servicioDialogo;
        public RelayCommand RefrescarListaProductosCommand { get; }
        public RelayCommand AñadirNuevoProductoCommand { get; }
        public RelayCommand EliminarProductoCommand { get; }
        public RelayCommand EditarProductoCommand { get; }


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
            servicioNavegacion = new ServicioNavegacion();
            servicioDialogo = new ServicioDialogo();
            CargarCategorias();
            CargarProductos();
            RefrescarListaProductosCommand = new RelayCommand(RefrescarListaProductos);
            AñadirNuevoProductoCommand = new RelayCommand(AñadirNuevoProducto);
            EliminarProductoCommand = new RelayCommand(EliminarProducto);
            EditarProductoCommand = new RelayCommand(EditarProducto);
            WeakReferenceMessenger.Default.Register<EnviarNuevoProductoMessage>(this, (r, m) =>
            {
                ListaProductos.Add(m.Value);
                servicioDialogo.MostrarMensajeInformacion("Producto añadido con exito", "Producto Añadido");    
            });

            WeakReferenceMessenger.Default.Register<ArticulosVM, EnviarProductoMessage>(this, (r, m) =>
             {
                 if (!m.HasReceivedResponse)
                 {
                     m.Reply(r.ProductoSeleccionado);
                 }
             });
            
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

        public void AñadirNuevoProducto()
        {
            ProductoSeleccionado = null;
            bool? resultado = servicioNavegacion.CargarAñadirEditarProducto();
        }

        public void EditarProducto()
        {
            if(ProductoSeleccionado != null)
            {
                bool? resultado = servicioNavegacion.CargarAñadirEditarProducto();
                if ((bool)resultado)
                {
                    servicioDialogo.MostrarMensajeInformacion("Producto modificado con exito!", "PRODUCTO MODIFICADO");
                }
                
                ProductoSeleccionado = null;
            }
        }

        public void EliminarProducto()
        {
            MessageBoxResult result = MessageBox.Show("¿Estas seguro de eliminar este producto?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(result == MessageBoxResult.Yes)
            {
                if (ProductoSeleccionado != null)
                {
                    IRestResponse response = servicioAPIRestRestaurante.DeleteProducto(ProductoSeleccionado.IdProducto);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {  
                        ListaProductos.Remove(ProductoSeleccionado);
                        servicioDialogo.MostrarMensajeInformacion("Producto eliminado con exito!", "Producto Eliminado");
                    }
                    else
                    {
                        servicioDialogo.MostrarMensajeError(response.Content, "Error al borrar el producto");
                    }
                }
            }
            
        }
    }
}
