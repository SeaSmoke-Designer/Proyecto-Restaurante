using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    class GestionarEmpleadosVM : ObservableObject
    {
        private readonly ServicioNavegacion servicioNavegacion;
        private readonly ServicioAPIRestRestaurante servicioAPIRestRestaurante;
        private readonly ServicioDialogo servicioDialogo;

        public RelayCommand AñadirNuevoEmpleadoCommand { get; }
        public RelayCommand EliminarEmpleadoCommand { get; }
        public RelayCommand EditarEmpleadoCommand { get; }

        private Empleado empleadoSeleccionado;
        public Empleado EmpleadoSeleccionado
        {
            get { return empleadoSeleccionado; }
            set { SetProperty(ref empleadoSeleccionado, value); }
        }

        private ObservableCollection<Empleado> listaEmpleados;
        public ObservableCollection<Empleado> ListaEmpleados
        {
            get { return listaEmpleados; }
            set { SetProperty(ref listaEmpleados, value); }
        }

        public GestionarEmpleadosVM()
        {
            servicioNavegacion = new ServicioNavegacion();
            servicioAPIRestRestaurante = new ServicioAPIRestRestaurante();
            servicioDialogo = new ServicioDialogo();
            CargarEmpleados();

        }

        public void CargarEmpleados()
        {
            ListaEmpleados = servicioAPIRestRestaurante.GetEmpleados();
        }
    }
}
