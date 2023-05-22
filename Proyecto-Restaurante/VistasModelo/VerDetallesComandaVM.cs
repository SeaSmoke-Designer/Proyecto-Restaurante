using CommunityToolkit.Mvvm.ComponentModel;
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
    class VerDetallesComandaVM : ObservableObject
    {
        //private readonly ServicioDialogo servicioDialogo;
        //private readonly ServicioAzure servicioAzure;
        private readonly ServicioAPIRestRestaurante servicioAPIRestRestaurante;
        private Comanda comandaRecibida;

        public Comanda ComandaRecibida
        {
            get { return comandaRecibida; }
            set { SetProperty(ref comandaRecibida, value); }
        }

        private ObservableCollection<DetalleComanda> listaDetallesComanda;

        public ObservableCollection<DetalleComanda> ListaDetallesComanda
        {
            get { return listaDetallesComanda; }
            set { SetProperty(ref listaDetallesComanda, value); }
        }


        private double totalComanda;
        public double TotalComanda
        {
            get { return totalComanda; }
            set { SetProperty(ref totalComanda, value);}
        }

        private string tituloComanda;

        public string TituloComanda
        {
            get { return "Comanda " + ComandaRecibida.IdComanda; }
            set { SetProperty(ref tituloComanda, value); }
        }


        public VerDetallesComandaVM()
        { 
            servicioAPIRestRestaurante = new ServicioAPIRestRestaurante();
            ComandaRecibida = WeakReferenceMessenger.Default.Send<EnviarComandaMessage>();
            if(ComandaRecibida != null)
            {
                ListaDetallesComanda = new ObservableCollection<DetalleComanda>();
                foreach (DetalleComanda item in servicioAPIRestRestaurante.GetDetallesComanda())
                {
                    if (item._DetallesComandaPK.IdComanda == ComandaRecibida.IdComanda)
                        ListaDetallesComanda.Add(item);
                }
            }
            CalcularImporteTotal();
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
                TotalComanda = result;
            }
        }
    }
}
