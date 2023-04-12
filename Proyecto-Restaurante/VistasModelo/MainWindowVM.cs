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
        private ServicioNavegacion servicioNavegacion;
        
        private UserControl contenidoVentana;
        public UserControl ContenidoVentana
        {
            get { return contenidoVentana; }
            set { SetProperty(ref contenidoVentana, value); }
        }

        public RelayCommand ConfiguracionCommand { get; }

        public MainWindowVM()
        {
            servicioNavegacion = new ServicioNavegacion();
            ConfiguracionCommand = new RelayCommand(Configuracion);
        }

        public void Configuracion()
        {
            ContenidoVentana = servicioNavegacion.CargarConfiguracion();
        }
    }
}
