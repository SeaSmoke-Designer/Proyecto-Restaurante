using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Proyecto_Restaurante.Mensajes;
using Proyecto_Restaurante.Modelos;
using Proyecto_Restaurante.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.VistasModelo
{
    class VerDetallesComandaVM : ObservableObject
    {
        //private readonly ServicioDialogo servicioDialogo;
        //private readonly ServicioAzure servicioAzure;
        //private readonly ServicioAPIRestRestaurante servicioAPIRestRestaurante;
        private Comanda comandaRecibida;

        public Comanda ComandaRecibida
        {
            get { return comandaRecibida; }
            set { SetProperty(ref comandaRecibida, value); }
        }

        private double totalComanda;
        public double TotalComanda
        {
            get {
                if (ComandaRecibida.DetallescomandaCollection != null)
                {
                    foreach (DetalleComanda item in ComandaRecibida.DetallescomandaCollection)
                    {
                        totalComanda += item.ImporteTotalProductos;

                    }
                }

                return totalComanda;
            }
            set {

                if (ComandaRecibida.DetallescomandaCollection != null)
                {
                    foreach (DetalleComanda item in ComandaRecibida.DetallescomandaCollection)
                    {
                        totalComanda += item.ImporteTotalProductos;
                        
                    }
                }
            }
        }

        private string tituloComanda;

        public string TituloComanda
        {
            get { return "Comanda " + ComandaRecibida.IdComanda; }
            set { SetProperty(ref tituloComanda, value); }
        }


        public VerDetallesComandaVM()
        {
            //servicioDialogo = new ServicioDialogo();
            //servicioAPIRestRestaurante = new ServicioAPIRestRestaurante();
            ComandaRecibida = WeakReferenceMessenger.Default.Send<EnviarComandaMessage>();
            
        }
    }
}
