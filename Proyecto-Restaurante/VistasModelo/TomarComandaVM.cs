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
        public RelayCommand ElegirProductoCommand { get; }

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


        private Comanda comandaActual;

        public Comanda ComandaActual
        {
            get { return comandaActual; }
            set { SetProperty(ref comandaActual, value); }
        }

        private string modo;
        public string Modo
        {
            get { return modo; }
            set { SetProperty(ref modo, value); }
        }


        public TomarComandaVM()
        {

            ComandaActual = new Comanda();

            WeakReferenceMessenger.Default.Register<TomarComandaVM, EnviarComandaMessage>(this, (r, m) =>
            {
                if (!m.HasReceivedResponse)
                {
                    m.Reply(r.ComandaActual);
                }
            });
            WeakReferenceMessenger.Default.Register<EnviarEmpleadoComandaMessage>(this, (r, m) =>
            {
                ComandaActual.Empleado = m.Value;
                NavegarListaProductos();
            });
            WeakReferenceMessenger.Default.Register<EnviarProductoComandaMessage>(this, (r, m) =>
            {
                AgregarProducto(m.Value);
            });
            WeakReferenceMessenger.Default.Register<EnviarMesaComandaMessage>(this, (r, m) =>
            {
                ComandaActual.Mesa = m.Value;
                NavegarListaProductos();
            });
            WeakReferenceMessenger.Default.Register<TomarComandaVM, EnviarImporteCobrar>(this, (r, m) =>
            {
                if (!m.HasReceivedResponse)
                {
                    m.Reply(r.TotalImporteComanda);
                }
            });

            servicioNavegacion = new ServicioNavegacion();
            servicioAPIRestRestaurante = new ServicioAPIRestRestaurante();
            servicioDialogo = new ServicioDialogo();
            ElegirEmpleadoCommand = new RelayCommand(NavegarListaEmpleados);
            ElegirMesaCommand = new RelayCommand(NavegarListaMesas);
            ElegirProductoCommand = new RelayCommand(NavegarListaProductos);
            BorrarLineaCommand = new RelayCommand(BorrarLinea);
            BorrarComandaCommand = new RelayCommand(BorrarComanda);
            PasarAMesaCommand = new RelayCommand(PasarAMesa);
            CobrarComandaCommand = new RelayCommand(CobrarComanda);
            NavegarListaProductos();
            DetallesComandaProductos = new ObservableCollection<DetalleComanda>();
        }
        public void NavegarListaEmpleados()
        {
            ContenidoVentana = servicioNavegacion.CargarListaEmpleados();
        }

        public void NavegarListaMesas()
        {
            if (ComandaActual.CantidadPersonas != 0)
            {
                ContenidoVentana = servicioNavegacion.CargarListaMesas();
            }
            else
            {
                servicioDialogo.MostrarMensajeAdvertencia("Faltan las cantidad de personas", "FALTA CANTIDAD PERSONAS");
            }

        }

        public void NavegarListaProductos()
        {
            ContenidoVentana = servicioNavegacion.CargarListaProductos();
        }

        public void AgregarProducto(Producto p)
        {
            if (DetallesComandaProductos.Count != 0)
            {
                if (!ContieneProducto(p))
                {
                    DetallesComandaProductos.Add(new DetalleComanda(1, p));
                }
            }
            else
            {
                DetallesComandaProductos.Add(new DetalleComanda(1, p));
            }
            CalcularImporteTotalComanda();

        }

        public bool ContieneProducto(Producto p)
        {
            for (int i = 0; i < DetallesComandaProductos.Count; i++)
            {
                if (DetallesComandaProductos[i].Producto.IdProducto == p.IdProducto)
                {
                    DetallesComandaProductos[i].Cantidad += 1;
                    DetallesComandaProductos[i].ActualizarImporteTotal();
                    return true;
                }
            }

            return false;
        }

        public void BorrarLinea()
        {
            WeakReferenceMessenger.Default.Send(new AvisarResetProductoMessage(DetalleComandaSeleccionada.Producto.IdProducto));
            DetallesComandaProductos.Remove(DetalleComandaSeleccionada);
            CalcularImporteTotalComanda();
            
        }

        public void BorrarComanda()
        {
            DetallesComandaProductos = new ObservableCollection<DetalleComanda>();
            CalcularImporteTotalComanda();
            WeakReferenceMessenger.Default.Send(new AvisarResetProductosMessage(true));
        }

        public void CalcularImporteTotalComanda()
        {
            double result = 0;
            if (DetallesComandaProductos != null)
            {
                foreach (DetalleComanda item in DetallesComandaProductos)
                {
                    result += item.ImporteTotalProducto;
                }
                TotalImporteComanda = result;
            }
        }

        public void PasarAMesa()
        {

            if (ComandaActual.Empleado != null)
            {
                if (ComandaActual.Mesa != null)
                {

                    if (ComandaActual.CantidadPersonas != 0)
                    {
                        ComandaActual.Fecha = DateTime.Now;
                        IRestResponse response = servicioAPIRestRestaurante.PostComanda(ComandaActual);
                        InsertarDetallesComanda(Int32.Parse(response.Content.Substring(4).Replace("\"", "").Replace("}", "")));
                        //servicioAPIRestRestaurante.PutMesa(ComandaActual.Mesa);

                        if (response.StatusCode == System.Net.HttpStatusCode.Created)
                        {
                            ComandaActual = new Comanda();
                            DetallesComandaProductos = new ObservableCollection<DetalleComanda>();
                        }
                        else if (response.StatusCode == System.Net.HttpStatusCode.UnsupportedMediaType)
                        {
                            servicioDialogo.MostrarMensajeError(response.Content, "ERROR - AL INSERTAR LA COMANDA");
                        }
                    }
                    else
                    {
                        servicioDialogo.MostrarMensajeAdvertencia("Falta la cantidad de personas", "FALTA CANTIDAD DE PERSONAS");
                    }

                }
                else
                {
                    servicioDialogo.MostrarMensajeAdvertencia("No hay ninguna mesa asignada", "FALTA ASIGNAR UNA MESA");
                }
            }
            else
            {
                servicioDialogo.MostrarMensajeAdvertencia("No hay ningun empleado asignado para esta comanda", "FALTA ASIGNAR UN EMPLEADO");
            }
        }



        public void InsertarDetallesComanda(int idComanda)
        {
            IRestResponse responseDetallesComanda;
            //IRestResponse responseProductos;
            foreach (DetalleComanda item in DetallesComandaProductos)
            {
                item._DetallesComandaPK = new DetalleComandaPK(idComanda, item.Producto.IdProducto);
                responseDetallesComanda = servicioAPIRestRestaurante.PostDetallesComanda(item);
                servicioAPIRestRestaurante.PutProducto(item.Producto);
                if (responseDetallesComanda.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    Console.WriteLine("OK");
                }
                else if (responseDetallesComanda.StatusCode == System.Net.HttpStatusCode.UnsupportedMediaType)
                {
                    servicioDialogo.MostrarMensajeError(responseDetallesComanda.Content, "ERROR - AL INSERTAR EL DETALLE COMANDA");
                }
            }
        }

        public void CobrarComanda()
        {
            ComandaActual.Fecha = DateTime.Now;
            bool? resultado = servicioNavegacion.CargarVentanaCobrarComanda();
            if ((bool)resultado)
            {
                ComandaActual.Pagada = true;
                IRestResponse response = servicioAPIRestRestaurante.PostComanda(ComandaActual);

                InsertarDetallesComanda(Int32.Parse(response.Content.Substring(4).Replace("\"", "").Replace("}", "")));
                //servicioAPIRestRestaurante.PutMesa(ComandaActual.Mesa);

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    ComandaActual = new Comanda();
                    DetallesComandaProductos = new ObservableCollection<DetalleComanda>();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.UnsupportedMediaType)
                {
                    servicioDialogo.MostrarMensajeError(response.Content, "ERROR - AL INSERTAR LA COMANDA");
                }
            }

        }
    }
}

