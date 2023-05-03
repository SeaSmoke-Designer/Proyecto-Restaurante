using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        private readonly ServicioNavegacion servicioNavegacion;
        public RelayCommand GestionarProductosCommand { get; }
        public RelayCommand GestionarEmpleadosCommand { get; }

        private UserControl contenidoVentana;
        public UserControl ContenidoVentana
        {
            get { return contenidoVentana; }
            set { SetProperty(ref contenidoVentana, value); }
        }

        public MainWindowVM()
        {
            servicioNavegacion = new ServicioNavegacion();
            GestionarProductosCommand = new RelayCommand(NavegarGestionarProductos);
            GestionarEmpleadosCommand = new RelayCommand(NavegarGestionEmpleados);
        }

        public void NavegarGestionarProductos()
        {
            ContenidoVentana = servicioNavegacion.CargarArticulos();
        }

        public void NavegarGestionEmpleados()
        {
            ContenidoVentana = servicioNavegacion.CargarGestionarEmpleados();
        }

        
    }
}
