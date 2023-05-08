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

namespace Proyecto_Restaurante.VistasModelo
{
    class AñadirEditarEmpleadoVM : ObservableObject
    {
        private readonly string imagenDefault = "../Assets/Add_ImageEmpleado.png";
        public RelayCommand GuardarEmpleadoCommand { get; }
        public RelayCommand NuevaImagenEmpleadoCommand { get; }

        private readonly ServicioDialogo servicioDialogo;
        private readonly ServicioAzure servicioAzure;
        private readonly ServicioAPIRestRestaurante servicioAPIRestRestaurante;

        private Empleado empleadoActual;

        public Empleado EmpleadoActual
        {
            get { return empleadoActual; }
            set { SetProperty(ref empleadoActual, value); }
        }

        private char password;

        public char Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }


        private string modoVentana;

        public string ModoVentana
        {
            get { return modoVentana; }
            set { SetProperty(ref modoVentana, value); }
        }

        public AñadirEditarEmpleadoVM()
        {
            servicioDialogo = new ServicioDialogo();
            servicioAzure = new ServicioAzure();
            servicioAPIRestRestaurante = new ServicioAPIRestRestaurante();
            EmpleadoActual = WeakReferenceMessenger.Default.Send<EnviarEmpleadoMessage>();
            if (EmpleadoActual is null)
            {
                EmpleadoActual = new Empleado();
                EmpleadoActual.URLFoto = imagenDefault;
            }
            ModoVentana = EmpleadoActual.IdEmpleado == 0 ? "Crear" : "Editar";
            NuevaImagenEmpleadoCommand = new RelayCommand(SeleccionarImagen);
            GuardarEmpleadoCommand = new RelayCommand(GuardarEmpleado);
        }

        public void SeleccionarImagen()
        {
            string file = servicioDialogo.DialogoAbrirFichero();
            EmpleadoActual.URLFoto = file != null ? servicioAzure.AlmacenarImagenEmpleadoNube(file) : imagenDefault;
        }


        public void GuardarEmpleado()
        {
            if (EmpleadoActual.IdEmpleado == 0)
            {
                AñadirNuevoEmpleado();
            }
            else
            {
                ActualizarProducto();
            }
        }

        public void AñadirNuevoEmpleado()
        {
            if (!ExisteEmpleado())
            {
                if (EmpleadoActual.Dni.Length == 9 || EmpleadoActual.Dni is null)
                {
                    if (EmpleadoActual.URLFoto.Equals(imagenDefault) || EmpleadoActual.URLFoto == null)
                    {
                        EmpleadoActual.URLFoto = "../Assets/SinImagen.png";
                    }

                    IRestResponse response = servicioAPIRestRestaurante.PostEmpleado(EmpleadoActual);
                    if (response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        WeakReferenceMessenger.Default.Send(new NuevoEmpleadoMessage(EmpleadoActual));
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.UnsupportedMediaType)
                    {
                        servicioDialogo.MostrarMensajeError(response.Content, "ERROR - NO SE PUDO AGREGAR EL NUEVO EMPLEADO");
                    }
                }
                else
                {
                    servicioDialogo.MostrarMensajeAdvertencia("El formato del DNI es incorrecto, por favor revise el DNI", "FORMATO DEL DNI INCORRECTO");
                }
            }
            else
            {
                servicioDialogo.MostrarMensajeAdvertencia("Ya existe empleado con ese DNI, por favor introduzca otro DNI", "EMPLEADO REPETIDO");
            }
        }

        public void ActualizarProducto()
        {
            if (EmpleadoActual.IdEmpleado != 0)
            {
                IRestResponse response = servicioAPIRestRestaurante.PutEmpleado(EmpleadoActual);
                if (response.StatusCode == System.Net.HttpStatusCode.UnsupportedMediaType)
                {
                    servicioDialogo.MostrarMensajeError(response.Content, "ERROR - NO SE PUEDE ACTUALIZAR EL PRODUCTO");
                }
            }

        }

        public bool ExisteEmpleado()
        {
            ObservableCollection<Empleado> listaEmpleados = servicioAPIRestRestaurante.GetEmpleados();
            foreach (Empleado item in listaEmpleados)
            {
                if (EmpleadoActual.Dni.Equals(item.Dni))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
