using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Proyecto_Restaurante.Mensajes;
using Proyecto_Restaurante.Modelos;
using Proyecto_Restaurante.Servicios;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            AñadirNuevoEmpleadoCommand = new RelayCommand(AñadirEmpleadoNuevo);
            EditarEmpleadoCommand = new RelayCommand(EditarEmpleado);
            EliminarEmpleadoCommand = new RelayCommand(EliminarEmpleado);

            WeakReferenceMessenger.Default.Register<NuevoEmpleadoMessage>(this, (r, m) =>
            {
                CargarEmpleados();
                servicioDialogo.MostrarMensajeInformacion("Empleado añadido con exito", "EMPLEADO AÑADIDO");
            });
            WeakReferenceMessenger.Default.Register<GestionarEmpleadosVM, EnviarEmpleadoMessage>(this, (r, m) =>
            {
                if (!m.HasReceivedResponse)
                {
                    m.Reply(r.EmpleadoSeleccionado);
                }
            });
        }

        public void CargarEmpleados()
        {
            ListaEmpleados = servicioAPIRestRestaurante.GetEmpleados();
        }

        public void AñadirEmpleadoNuevo()
        {
            EmpleadoSeleccionado = null;
            bool? resultado = servicioNavegacion.CargarAñadirEditarEmpleado();
        }

        public void EditarEmpleado()
        {
            if (EmpleadoSeleccionado != null)
            {
                bool? resultado = servicioNavegacion.CargarAñadirEditarEmpleado();
                if ((bool)resultado)
                {
                    servicioDialogo.MostrarMensajeInformacion("Empleado modificado con exito!", "EMPLEADO MODIFICADO");
                }

                EmpleadoSeleccionado = null;
            }
        }

        public void EliminarEmpleado()
        {
            MessageBoxResult result = MessageBox.Show("¿Estas seguro de eliminar este empleado?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                if (EmpleadoSeleccionado != null)
                {
                    IRestResponse response = servicioAPIRestRestaurante.DeleteEmpleado(EmpleadoSeleccionado.IdEmpleado);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        ListaEmpleados.Remove(EmpleadoSeleccionado);
                        servicioDialogo.MostrarMensajeInformacion("Empleado eliminado con exito!", "EMPLEADO ELIMINADO");
                    }
                    else
                    {
                        servicioDialogo.MostrarMensajeError(response.Content, "ERROR - NO SE PUEDO BORRAR EL EMPLEADO");
                    }
                }
            }

        }
    }
}
