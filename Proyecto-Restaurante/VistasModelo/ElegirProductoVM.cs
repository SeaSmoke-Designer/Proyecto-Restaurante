using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Proyecto_Restaurante.Mensajes;
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
    class ElegirProductoVM : ObservableObject
    {
        private readonly ServicioAPIRestRestaurante servicioAPIRestRestaurante;
        private readonly ServicioDialogo servicioDialogo;

        public RelayCommand RefrescarListaProductosCommand { get; }

        private ObservableCollection<Categoria> listaCategorias;

        public ObservableCollection<Categoria> ListaCategorias
        {
            get { return listaCategorias; }
            set { SetProperty(ref listaCategorias, value); }
        }

        private ObservableCollection<Producto> listaProductos;

        public ObservableCollection<Producto> ListaProductos
        {
            get { return listaProductos; }
            set { SetProperty(ref listaProductos, value); }
        }

        private Categoria categoriaSeleccionada;

        public Categoria CategoriaSeleccionada
        {
            get { return categoriaSeleccionada; }
            set
            {
                SetProperty(ref categoriaSeleccionada, value);
                OnPropertyChanged(nameof(CategoriaSeleccionada));
                FiltrarProductosPorCategoria();
            }
        }

        private Producto productoSeleccionado;

        public Producto ProductoSeleccionado
        {
            get { return productoSeleccionado; }
            set
            {
                SetProperty(ref productoSeleccionado, value);
                if(ProductoSeleccionado != null)
                {
                    OnPropertyChanged(nameof(ProductoSeleccionado));
                    EnviarProductoComanda();
                }
            }
        }

        public ElegirProductoVM()
        {
            servicioAPIRestRestaurante = new ServicioAPIRestRestaurante();
            servicioDialogo = new ServicioDialogo();
            RefrescarListaProductosCommand = new RelayCommand(RefrescarListaProductos);
            CargarProductos();
            CargarCategorias();
            WeakReferenceMessenger.Default.Register<AvisarResetProductosMessage>(this, (r, m) =>
            {
                CargarProductos();
            });
            WeakReferenceMessenger.Default.Register<AvisarResetProductoMessage>(this, (r, m) =>
            {
                ResetearUnidadesProducto(m.Value);
            });
        }

        public void CargarProductos()
        {
            ListaProductos = servicioAPIRestRestaurante.GetProductos();
        }

        public void CargarCategorias()
        {
            ListaCategorias = servicioAPIRestRestaurante.GetCategorias();
        }

        public void FiltrarProductosPorCategoria()
        {
            ObservableCollection<Producto> listaAux = new ObservableCollection<Producto>();
            ObservableCollection<Producto> listaAux2 = servicioAPIRestRestaurante.GetProductos();
            if (CategoriaSeleccionada != null)
            {
                foreach (Producto item in listaAux2)
                {
                    if (item.IdCategoria.NombreCategoria == CategoriaSeleccionada.NombreCategoria)
                        listaAux.Add(item);
                }
                ListaProductos = listaAux;
            }
        }

        public void RefrescarListaProductos()
        {
            CargarProductos();
            CategoriaSeleccionada = null;
        }

        public void EnviarProductoComanda()
        {
            if (ProductoSeleccionado != null)
            {
                if (ProductoSeleccionado.UnidadesEnAlmacen != 0)
                {
                    ProductoSeleccionado.UnidadesEnAlmacen -= 1;
                    WeakReferenceMessenger.Default.Send(new EnviarProductoComandaMessage(ProductoSeleccionado));
                    ProductoSeleccionado = null;
                }
                else if (ProductoSeleccionado.UnidadesEnAlmacen == 0)
                {
                    servicioDialogo.MostrarMensajeAdvertencia($"No quedan unidades del producto {ProductoSeleccionado.NombreProducto}", "NO QUEDAN UNIDADES");
                }
            }
        }

        public void ResetearUnidadesProducto(int id)
        {
            for (int i = 0; i < ListaProductos.Count; i++)
            {
                if (ListaProductos[i].IdProducto == id)
                    ListaProductos[i] = servicioAPIRestRestaurante.GetProducto(id);
            }
        }

        
    }
}
