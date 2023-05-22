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
    class ElegirEmpleadoVM : ObservableObject
    {
        private readonly ServicioAPIRestRestaurante servicioAPIRestRestaurante;
        //private readonly ServicioDialogo servicioDialogo;
        //private readonly ServicioNavegacion servicioNavegacion;

        private ObservableCollection<Empleado> listaEmpleados;
        public ObservableCollection<Empleado> ListaEmpleados
        {
            get { return listaEmpleados; }
            set { SetProperty(ref listaEmpleados, value); }
        }

        private Empleado empleadoSeleccionado;

        public Empleado EmpleadoSeleccionado
        {
            get { return empleadoSeleccionado; }
            set
            {
                SetProperty(ref empleadoSeleccionado, value);
                OnPropertyChanged(nameof(EmpleadoSeleccionado));
                EnviarEmpleadoSeleccionado();
            }
        }

        public ElegirEmpleadoVM()
        {
            servicioAPIRestRestaurante = new ServicioAPIRestRestaurante();
            CargarEmpleados();
        }

        public void CargarEmpleados()
        {
            var aux = servicioAPIRestRestaurante.GetEmpleados().Where(n => n.Cargo.Equals("Camarero") || n.Cargo.Equals("Camarera"));
            ListaEmpleados = new ObservableCollection<Empleado>();
            foreach (Empleado item in aux)
            {
                ListaEmpleados.Add(item);
            }
        }

        public void EnviarEmpleadoSeleccionado()
        {
            WeakReferenceMessenger.Default.Send(new EnviarEmpleadoComandaMessage(EmpleadoSeleccionado));
        }
    }
}
