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
    class GestionarComandasVM : ObservableObject
    {
        public RelayCommand VerComandaCommand { get; }
        public RelayCommand EditarComandaCommand { get; }

        private readonly ServicioNavegacion servicioNavegacion;
        private readonly ServicioDialogo servicioDialogo;
        private readonly ServicioAPIRestRestaurante servicioAPIRestRestaurante;

        private ObservableCollection<Comanda> listaComandas;

        public ObservableCollection<Comanda> ListaComandas
        {
            get { return listaComandas; }
            set { SetProperty(ref listaComandas, value); }
        }

        private Comanda comandaSeleccionada;

        public Comanda ComandaSeleccionada
        {
            get { return comandaSeleccionada; }
            set { SetProperty(ref comandaSeleccionada, value); }
        }


        public GestionarComandasVM()
        {
            servicioNavegacion = new ServicioNavegacion();
            servicioDialogo = new ServicioDialogo();
            servicioAPIRestRestaurante = new ServicioAPIRestRestaurante();
            VerComandaCommand = new RelayCommand(VerDetallesComanda);
            WeakReferenceMessenger.Default.Register<GestionarComandasVM, EnviarComandaMessage>(this, (r, m) =>
            {
                if (!m.HasReceivedResponse)
                {
                    m.Reply(r.ComandaSeleccionada);
                }
            });
            CargarComandas();
        }

        public void CargarComandas()
        {
            ListaComandas = servicioAPIRestRestaurante.GetComandasPagadas(false);
        }

        public void VerDetallesComanda()
        {
            if (ComandaSeleccionada != null)
            {
                bool? result = servicioNavegacion.CargarVerDetallesComanda();
            }
        }



    }
}
