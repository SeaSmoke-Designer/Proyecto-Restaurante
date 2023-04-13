using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Proyecto_Restaurante.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.VistasModelo
{
    class ConfiguracionVM : ObservableObject
    {
        private ServicioNavegacion servicioNavegacion;

        public RelayCommand ArticulosCommand { get; }

        public ConfiguracionVM()
        {
            servicioNavegacion = new ServicioNavegacion();
        }


    }
}
