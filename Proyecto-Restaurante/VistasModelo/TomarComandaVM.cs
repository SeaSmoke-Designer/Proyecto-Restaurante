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
using System.Windows.Controls;

namespace Proyecto_Restaurante.VistasModelo
{
    class TomarComandaVM : ObservableObject
    {
        private readonly ServicioAPIRestRestaurante servicioAPIRestRestaurante;
        private readonly ServicioDialogo servicioDialogo;
        private readonly ServicioNavegacion servicioNavegacion;

        public RelayCommand BorrarLineaCommand { get; }
        public RelayCommand BorrarComandaCommand { get; }
        public RelayCommand PasarAMesaCommand { get; }
        public RelayCommand CobrarComandaCommand { get; }
        public RelayCommand ElegirEmpleadoCommand { get; }
        public RelayCommand ElegirMesaCommand { get; }

        private UserControl contenidoVentana;
        public UserControl ContenidoVentana
        {
            get { return contenidoVentana; }
            set { SetProperty(ref contenidoVentana, value); }
        }

        private ObservableCollection<DetalleComanda> detallesComandaProductos;

        public ObservableCollection<DetalleComanda> DetallesComandaProductos
        {
            get { return detallesComandaProductos; }
            set { SetProperty(ref detallesComandaProductos, value); }
        }
        
        /*private ObservableCollection<Producto> listaProductos;

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
        private Producto productoSeleccionado;

        public Producto ProductoSeleccionado
        {
            get { return productoSeleccionado; }
            set
            {
                SetProperty(ref productoSeleccionado, value);
                OnPropertyChanged(nameof(ProductoSeleccionado));
            }
        }

        private Categoria categoriaSeleccionada;

        public Categoria CategoriaSeleccionada
        {
            get { return categoriaSeleccionada; }
            set
            {
                SetProperty(ref categoriaSeleccionada, value);
                OnPropertyChanged(nameof(CategoriaSeleccionada));
            }
        }*/

        private Comanda comandaTomada;

        public Comanda ComandaTomada
        {
            get { return comandaTomada; }
            set { SetProperty(ref comandaTomada, value); }
        }



        public TomarComandaVM()
        {
            ComandaTomada = new Comanda();
            servicioNavegacion = new ServicioNavegacion();
            servicioAPIRestRestaurante = new ServicioAPIRestRestaurante();
            servicioDialogo = new ServicioDialogo();
            //CargarProductos();
            //CargarCategorias();
            ElegirEmpleadoCommand = new RelayCommand(NavegarListaEmpleados);
            ElegirMesaCommand = new RelayCommand(NavegarListaMesas);
            WeakReferenceMessenger.Default.Register<TomarComandaVM, EnviarComandaMessage>(this, (r, m) =>
            {
                if (!m.HasReceivedResponse)
                {
                    m.Reply(r.ComandaTomada);
                }
            });
        }

        public void CargarProductos()
        {
            //ListaProductos = servicioAPIRestRestaurante.GetProductos();
        }

        public void CargarCategorias()
        {
            //ListaCategorias = servicioAPIRestRestaurante.GetCategorias();
        }

        public void NavegarListaEmpleados()
        {
            ContenidoVentana = servicioNavegacion.CargarListaEmpleados();
        }

        public void NavegarListaMesas()
        {
            if (ComandaTomada.CantidadPersonas != 0)
            {
                ContenidoVentana = servicioNavegacion.CargarListaMesas();
            }
            else
            {
                servicioDialogo.MostrarMensajeAdvertencia("Faltan las cantidad de personas", "FALTA CANTIDAD PERSONAS");
            }
            
        }


    }
}
