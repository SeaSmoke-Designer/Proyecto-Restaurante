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
    class ElegirMesaVM : ObservableObject
    {
        private readonly ServicioAPIRestRestaurante servicioAPIRestRestaurante;

        private ObservableCollection<Mesa> listaMesas;

        public ObservableCollection<Mesa> ListaMesas
        {
            get { return listaMesas; }
            set { SetProperty(ref listaMesas, value); }
        }

        private Mesa mesaSeleccionada;

        public Mesa MesaSeleccionada
        {
            get { return mesaSeleccionada; }
            set { SetProperty(ref mesaSeleccionada, value); }
        }

        private Comanda comandaActual;
        public Comanda ComandaActual
        {
            get { return comandaActual; }
            set { SetProperty(ref comandaActual, value); }
        }


        public ElegirMesaVM()
        {
            servicioAPIRestRestaurante = new ServicioAPIRestRestaurante();
            CargarMesas();
            ComandaActual = WeakReferenceMessenger.Default.Send<EnviarComandaMessage>();
        }

        public void CargarMesas()
        {
            ObservableCollection<Comanda> auxComandas = servicioAPIRestRestaurante.GetComandas();
            ListaMesas = new ObservableCollection<Mesa>();
            ListaMesas = servicioAPIRestRestaurante.GetMesas();
            /*List<int> idsMesas = new List<int>();
            foreach (Comanda item in auxComandas)
            {
                //idsMesas.Add(item.Mesa.IdMesa);
                
            }*/

            for (int i = 0; i < auxComandas.Count; i++)
            {
                if(auxComandas[i].Mesa.IdMesa == ListaMesas[i].IdMesa)
                {
                    ListaMesas[i].MesaOcupada = true;
                }
                else
                {
                    ListaMesas[i].MesaOcupada = false;
                }
            }
            
        }


        public void EnviarMesaSeleccionada()
        {
            
        }


    }
}
