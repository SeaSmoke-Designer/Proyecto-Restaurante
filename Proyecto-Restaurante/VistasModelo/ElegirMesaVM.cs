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
        private readonly ServicioDialogo servicioDialogo;

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
            set
            {
                SetProperty(ref mesaSeleccionada, value);
                OnPropertyChanged(nameof(MesaSeleccionada));
                EnviarMesaSeleccionada();
            }
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
            servicioDialogo = new ServicioDialogo();
            ComandaActual = WeakReferenceMessenger.Default.Send<EnviarComandaMessage>();
            CargarMesas();

        }

        public void CargarMesas()
        {
            ObservableCollection<Comanda> auxComandas = servicioAPIRestRestaurante.GetComandas();
            ListaMesas = new ObservableCollection<Mesa>();
            ListaMesas = servicioAPIRestRestaurante.GetMesas();
            foreach (Comanda comanda in auxComandas)
            {
                foreach (Mesa mesa in ListaMesas)
                {
                    if (comanda.Mesa.IdMesa == mesa.IdMesa)
                    {
                        mesa.MesaOcupada = true;
                    }

                }
            }
            ComprobarCapacidad();

        }

        private void ComprobarCapacidad()
        {
            foreach (Mesa item in ListaMesas)
            {
                if (ComandaActual.CantidadPersonas > item.Capacidad)
                {
                    item.CapacidadIsOk = false;
                }
                else if (ComandaActual.CantidadPersonas <= item.Capacidad)
                {
                    item.CapacidadIsOk = true;
                }
            }
        }


        public void EnviarMesaSeleccionada()
        {
            if (!MesaSeleccionada.MesaOcupada)
            {
                if (MesaSeleccionada.CapacidadIsOk)
                {
                    MesaSeleccionada.MesaOcupada = true;
                    WeakReferenceMessenger.Default.Send(new EnviarMesaComandaMessage(MesaSeleccionada));
                }
                else
                {
                    servicioDialogo.MostrarMensajeAdvertencia("La capacidad de la mesa es inferior a la cantidad de personas", "CAPACIDAD DE LA MESA INFERIOR");
                }
            }
            else
            {
                servicioDialogo.MostrarMensajeAdvertencia("Esta mesa ya esta ocupa, eliga otra", "MESA OCUPADA");
            }

        }


    }
}
