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

namespace Proyecto_Restaurante.VistasModelo
{
    class VentanaCobrarComandaVM : ObservableObject
    {
        

        public RelayCommand CobrarComandaCommand { get; }

        private double precioCobrar;

        public double PrecioCobrar
        {
            get { return precioCobrar; }
            set { SetProperty(ref precioCobrar, value); }
        }

        public VentanaCobrarComandaVM()
        {
            PrecioCobrar = WeakReferenceMessenger.Default.Send<EnviarImporteCobrar>();
        }


    }
}
