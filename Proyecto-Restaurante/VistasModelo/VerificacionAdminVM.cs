using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Proyecto_Restaurante.Mensajes;
using Proyecto_Restaurante.Modelos;
using Proyecto_Restaurante.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.VistasModelo
{
    class VerificacionAdminVM : ObservableObject
    {
        private readonly ServicioAPIRestRestaurante servicioAPIRestRestaurante;
        private readonly ServicioDialogo servicioDialogo;
        public RelayCommand ValidarContraseñaCommand { get; }

        private string dniIntroducido;

        public string DniIntroducido
        {
            get { return dniIntroducido; }
            set { SetProperty(ref dniIntroducido, value); }
        }


        private string contraseñaIntroducida;
        public string ContraseñaIntroducida
        {
            get { return contraseñaIntroducida; }
            set { SetProperty(ref contraseñaIntroducida, value); }
        }

        private Empleado empleadoAdmin;
        public Empleado EmpleadoAdmin
        {
            get { return empleadoAdmin; }
            set { SetProperty(ref empleadoAdmin, value); }
        }

        private Encrypt encriptador;

        public Encrypt Encriptador
        {
            get { return encriptador; }
            set { SetProperty(ref encriptador, value); }
        }

        public VerificacionAdminVM()
        {
            servicioDialogo = new ServicioDialogo();
            servicioAPIRestRestaurante = new ServicioAPIRestRestaurante();
            ValidarContraseñaCommand = new RelayCommand(ValidarContraseña);
            Encriptador = new Encrypt();
        }

        public void ValidarContraseña()
        {
            EmpleadoAdmin = servicioAPIRestRestaurante.GetEmpleadoByDni(DniIntroducido);
            if(EmpleadoAdmin != null || EmpleadoAdmin.IdEmpleado != 0)
            {
                if (EmpleadoAdmin.Cargo.Equals("Admin"))
                {
                    if (Encriptador.DesEncriptar(EmpleadoAdmin.ContraseñaEmpleado).Equals(ContraseñaIntroducida))
                    {
                        WeakReferenceMessenger.Default.Send(new EnviarValidacionAdminMessage(true));
                    }
                    else WeakReferenceMessenger.Default.Send(new EnviarValidacionAdminMessage(false));
                }
                else servicioDialogo.MostrarMensajeError("El empleado indicado no es administrador","ERROR");
                
            }
            else
            {
                servicioDialogo.MostrarMensajeError($"El empleado con el DNI {EmpleadoAdmin.Dni} no existe", "ERROR - EMPLEADO NO EXISTE");
            }
        }
    }
}
