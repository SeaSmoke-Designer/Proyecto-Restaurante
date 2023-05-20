using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Proyecto_Restaurante.Mensajes;
using Proyecto_Restaurante.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Proyecto_Restaurante.VistasModelo
{
    class MainWindowVM : ObservableObject
    {
        private readonly ServicioDialogo servicioDialogo;
        private readonly ServicioNavegacion servicioNavegacion;
        public RelayCommand GestionarProductosCommand { get; }
        public RelayCommand GestionarEmpleadosCommand { get; }
        public RelayCommand SalirAplicacionCommand { get; }
        public RelayCommand HistoricoComandasCommand { get; }
        public RelayCommand GestionarMesasCommand { get; }
        public RelayCommand GestionarComandasCommand { get; }
        public RelayCommand TomarComandaCommand { get; }

        private bool validacionAdmin;

        public bool ValidacionAdmin
        {
            get { return validacionAdmin; }
            set { SetProperty(ref validacionAdmin, value); }
        }


        private UserControl contenidoVentana;
        public UserControl ContenidoVentana
        {
            get { return contenidoVentana; }
            set { SetProperty(ref contenidoVentana, value); }
        }

        public MainWindowVM()
        {
            servicioDialogo = new ServicioDialogo();
            servicioNavegacion = new ServicioNavegacion();
            GestionarProductosCommand = new RelayCommand(NavegarGestionarProductos);
            GestionarEmpleadosCommand = new RelayCommand(NavegarGestionEmpleados);
            SalirAplicacionCommand = new RelayCommand(CerrarAplicacion);
            HistoricoComandasCommand = new RelayCommand(NavegarHistoricoComandas);
            GestionarMesasCommand = new RelayCommand(NavegarGestionarMesas);
            GestionarComandasCommand = new RelayCommand(NavegarGestionarComandas);
            TomarComandaCommand = new RelayCommand(NavegarTomarComanda);
            WeakReferenceMessenger.Default.Register<EnviarValidacionAdminMessage>(this, (r, m) =>
            {
                ValidacionAdmin = m.Value;
            });

        }

        public void NavegarGestionarProductos()
        {
            ContenidoVentana = servicioNavegacion.CargarArticulos();
        }

        public void NavegarGestionEmpleados()
        {
            /*if(ContenidoVentana == servicioNavegacion.CargarGestionarEmpleados())
            {
                ContenidoVentana = new UserControl();
            }*/
            bool? reult = servicioNavegacion.CargarValidacionAdmin();
            if ((bool)reult)
            {
                if (ValidacionAdmin)
                {
                    ContenidoVentana = servicioNavegacion.CargarGestionarEmpleados();
                }
                else servicioDialogo.MostrarMensajeError("Contraseña Incorrecta, por favor, vuelva a intentarlo", "ERROR - CONTRASEÑA INCORRECTA");
            }
        }

        public void NavegarHistoricoComandas()
        {
            ContenidoVentana = servicioNavegacion.CargarHistoricoComandas();
        }

        public void NavegarGestionarMesas()
        {
            ContenidoVentana = servicioNavegacion.CargarGestionarMesas();
        }

        public void NavegarGestionarComandas()
        {
            ContenidoVentana = servicioNavegacion.CargarGestionarComandas();
        }
        public void NavegarTomarComanda()
        {
            ContenidoVentana = servicioNavegacion.CargarTomarComanda();
        }

        public void CerrarAplicacion()
        {
            App.Current.Shutdown();
        }

        
    }
}
