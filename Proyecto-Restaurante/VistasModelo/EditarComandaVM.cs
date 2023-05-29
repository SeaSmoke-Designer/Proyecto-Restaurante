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

namespace Proyecto_Restaurante.VistasModelo
{
    class EditarComandaVM : ObservableObject
    {
        public RelayCommand GuardarComandaCommand { get; }
        public RelayCommand RefrescarListaProductosCommand { get; }
        public RelayCommand BorrarLineaCommand { get; }
        public RelayCommand BorrarComandaCommand { get; }
        public RelayCommand CobrarComandaCommand { get; }
        public RelayCommand SumarCantidadProductoCommand { get; }
        public RelayCommand RestarCantidadProductoCommand { get; }
        private readonly ServicioDialogo servicioDialogo;
        private readonly ServicioAPIRestRestaurante servicioAPIRestRestaurante;
        private readonly ServicioNavegacion servicioNavegacion;

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

        private ObservableCollection<DetalleComanda> listaDetallesComanda;

        public ObservableCollection<DetalleComanda> ListaDetallesComanda
        {
            get { return listaDetallesComanda; }
            set { SetProperty(ref listaDetallesComanda, value); }
        }


        private Categoria categoriaSeleccionada;

        public Categoria CategoriaSeleccionada
        {
            get { return categoriaSeleccionada; }
            set { SetProperty(ref categoriaSeleccionada, value);
                OnPropertyChanged(nameof(CategoriaSeleccionada));
                FiltrarProductosPorCategoria();
            }
        }

        private Producto productoSeleccionado;

        public Producto ProductoSeleccionado
        {
            get { return productoSeleccionado;; }
            set { SetProperty(ref productoSeleccionado, value);
                if (ProductoSeleccionado != null)
                {
                    OnPropertyChanged(nameof(ProductoSeleccionado));
                    AgregarProducto();
                }
            }
        }


        private Comanda comandaRecibida;

        public Comanda ComandaRecibida
        {
            get { return comandaRecibida; }
            set { SetProperty(ref comandaRecibida, value); }
        }

        private DetalleComanda detalleComandaSeleccionada;

        public DetalleComanda DetalleComandaSeleccionada
        {
            get { return detalleComandaSeleccionada; }
            set { SetProperty(ref detalleComandaSeleccionada, value); }
        }


        private double totalImporteComanda;

        public double TotalImporteComanda
        {
            get { return totalImporteComanda; }
            set { SetProperty(ref totalImporteComanda, value); }
        }



        public EditarComandaVM()
        {
            servicioAPIRestRestaurante = new ServicioAPIRestRestaurante();
            servicioDialogo = new ServicioDialogo();
            servicioNavegacion = new ServicioNavegacion();
            RefrescarListaProductosCommand = new RelayCommand(RefrescarListaProductos);
            BorrarLineaCommand = new RelayCommand(BorrarLinea);
            BorrarComandaCommand = new RelayCommand(BorrarComanda);
            GuardarComandaCommand = new RelayCommand(EditarComanda);
            CobrarComandaCommand = new RelayCommand(CobrarComanda);
            SumarCantidadProductoCommand = new RelayCommand(SumarCantidadProducto);
            RestarCantidadProductoCommand = new RelayCommand(RestarCantidadProducto);
            WeakReferenceMessenger.Default.Register<EditarComandaVM, EnviarImporteCobrar>(this, (r, m) =>
            {
                if (!m.HasReceivedResponse)
                {
                    m.Reply(r.TotalImporteComanda);
                }
            });
            ComandaRecibida = WeakReferenceMessenger.Default.Send<EnviarComandaMessage>();
            if (ComandaRecibida != null)
            {
                ListaDetallesComanda = new ObservableCollection<DetalleComanda>();
                foreach (DetalleComanda item in servicioAPIRestRestaurante.GetDetallesComanda())
                {
                    if (item._DetallesComandaPK.IdComanda == ComandaRecibida.IdComanda)
                        ListaDetallesComanda.Add(item);
                }
                CalcularImporteTotal();
            }

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

        public void CalcularImporteTotal()
        {
            double result = 0;
            if (ListaDetallesComanda != null)
            {
                foreach (DetalleComanda item in ListaDetallesComanda)
                {
                    result += item.ImporteTotalProducto;
                }
                TotalImporteComanda = result;
            }
        }

        public void AgregarProducto()
        {
            if (ProductoSeleccionado != null)
            {
                if (ProductoSeleccionado.UnidadesEnAlmacen != 0)
                {
                    ProductoSeleccionado.UnidadesEnAlmacen -= 1;
                    if (ListaDetallesComanda.Count != 0)
                    {
                        if (!ContieneProducto())
                        {
                            ListaDetallesComanda.Add(new DetalleComanda(1, ProductoSeleccionado));
                        }
                    }
                    else
                    {
                        ListaDetallesComanda.Add(new DetalleComanda(1, ProductoSeleccionado));
                    }
                    CalcularImporteTotal();
                    ProductoSeleccionado = null;
                }
                else if (ProductoSeleccionado.UnidadesEnAlmacen == 0)
                {
                    servicioDialogo.MostrarMensajeAdvertencia($"No quedan unidades del producto {ProductoSeleccionado.NombreProducto}", "NO QUEDAN UNIDADES");
                }
            }
        }

        public bool ContieneProducto()
        {
            for (int i = 0; i < ListaDetallesComanda.Count; i++)
            {
                if (ListaDetallesComanda[i].Producto.IdProducto == ProductoSeleccionado.IdProducto)
                {
                    ListaDetallesComanda[i].Cantidad += 1;
                    ListaDetallesComanda[i].ActualizarImporteTotal();
                    return true;
                }
            }
            return false;
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

        public void EditarComanda()
        {
            if(ListaDetallesComanda.Count != 0)
            {
                ComandaRecibida.Fecha = DateTime.Now;
                IRestResponse response = servicioAPIRestRestaurante.PutComanda(ComandaRecibida);
                EditarDetallesComanda();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    servicioDialogo.MostrarMensajeInformacion("Comanda Actualizada", "COMANDA ACTUALIZADA");

                }
                else if (response.StatusCode == System.Net.HttpStatusCode.UnsupportedMediaType)
                {
                    servicioDialogo.MostrarMensajeError(response.Content, "ERROR - AL INSERTAR LA COMANDA");
                }
            }
        }

        public void EditarDetallesComanda()
        {
            IRestResponse responseDetallesComanda = null;
            
            foreach (DetalleComanda item in ListaDetallesComanda)
            {
                item._DetallesComandaPK = new DetalleComandaPK(ComandaRecibida.IdComanda, item.Producto.IdProducto);

                IRestResponse response = servicioAPIRestRestaurante.GetDetalleComanda(item._DetallesComandaPK.IdComanda,item._DetallesComandaPK.IdProducto);
                if(response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    responseDetallesComanda = servicioAPIRestRestaurante.PutDetallesComanda(item);

                }else if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    responseDetallesComanda = servicioAPIRestRestaurante.PostDetallesComanda(item);
                }
                
                servicioAPIRestRestaurante.PutProducto(item.Producto);
                
                if (responseDetallesComanda.StatusCode == System.Net.HttpStatusCode.UnsupportedMediaType)
                {
                    servicioDialogo.MostrarMensajeError(responseDetallesComanda.Content, "ERROR - AL INSERTAR EL DETALLE COMANDA");
                }
            }
        }

        
        public void BorrarLinea()
        {
            ListaDetallesComanda.Remove(DetalleComandaSeleccionada);
            CalcularImporteTotal();
        }

        public void BorrarComanda()
        {
            ListaDetallesComanda = new ObservableCollection<DetalleComanda>();
            CalcularImporteTotal();
        }

        public void CobrarComanda()
        {
            ComandaRecibida.Fecha = DateTime.Now;
            bool? resultado = servicioNavegacion.CargarVentanaCobrarComanda();
            if ((bool)resultado)
            {
                ComandaRecibida.Pagada = true;
                IRestResponse response = servicioAPIRestRestaurante.PutComanda(ComandaRecibida);
                EditarDetallesComanda();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    servicioDialogo.MostrarMensajeInformacion("Comanda finalizada con exito!", "COMANDA FINALIZADA");
                    ComandaRecibida = new Comanda();
                    ListaDetallesComanda = new ObservableCollection<DetalleComanda>();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.UnsupportedMediaType)
                {
                    servicioDialogo.MostrarMensajeError(response.Content, "ERROR - AL INSERTAR LA COMANDA");
                }
            }

        }

        public void SumarCantidadProducto()
        {
            if (DetalleComandaSeleccionada != null)
            {
                if (DetalleComandaSeleccionada.Producto.UnidadesEnAlmacen != 0)
                {
                    DetalleComandaSeleccionada.Cantidad += 1;
                    DetalleComandaSeleccionada.Producto.UnidadesEnAlmacen -= 1;
                    CambiarUnidadesEnAlmacen(DetalleComandaSeleccionada);
                }
                else
                {
                    servicioDialogo.MostrarMensajeAdvertencia("No quedan mas unidades en el almacen", "NO QUENDA UNIDADES");
                }

            }

            CalcularImporteTotal();
        }
        public void RestarCantidadProducto()
        {
            if (DetalleComandaSeleccionada != null)
            {
                if (DetalleComandaSeleccionada.Cantidad > 1)
                {
                    DetalleComandaSeleccionada.Cantidad -= 1;
                    DetalleComandaSeleccionada.Producto.UnidadesEnAlmacen += 1;
                    CambiarUnidadesEnAlmacen(DetalleComandaSeleccionada);
                }
                else
                {
                    DetalleComandaSeleccionada.Producto.UnidadesEnAlmacen += 1;
                    CambiarUnidadesEnAlmacen(DetalleComandaSeleccionada);
                    ListaDetallesComanda.Remove(DetalleComandaSeleccionada);
                }
                CalcularImporteTotal();

               
            }
        }

        public void CambiarUnidadesEnAlmacen(DetalleComanda d)
        {
            for (int i = 0; i < ListaProductos.Count; i++)
            {
                if (ListaProductos[i].IdProducto == d.Producto.IdProducto)
                {
                    ListaProductos[i].UnidadesEnAlmacen = d.Producto.UnidadesEnAlmacen;
                    return;
                }

            }
        }
       
    }
}
