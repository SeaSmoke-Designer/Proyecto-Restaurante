using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.Modelos
{
    class Empleados : ObservableObject
    {
        private int idEmpleado;

        public int IdEmpleado
        {
            get { return idEmpleado; }
            set { SetProperty(ref idEmpleado, value); }
        }

        private string dni;

        public string Dni
        {
            get { return dni; }
            set { SetProperty(ref dni, value); }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { SetProperty(ref nombre, value); }
        }


        private string apellido;

        public string Apellido
        {
            get { return apellido; }
            set { SetProperty(ref apellido, value); }
        }


        private string cargo;

        public string Cargo
        {
            get { return cargo; }
            set { SetProperty(ref cargo, value); }
        }

        private DateTime fechaNacimiento;

        public DateTime FechaNacimiento
        {
            get { return fechaNacimiento; }
            set { SetProperty(ref fechaNacimiento, value); }
        }

        private string direccion;

        public string Direccion
        {
            get { return direccion; }
            set { SetProperty(ref direccion, value); }
        }

        private string telefonoParticular;

        public string TelefonoParticular
        {
            get { return telefonoParticular; }
            set { SetProperty(ref telefonoParticular, value); }
        }

        private string urlFoto;

        public string URLFoto
        {
            get { return urlFoto; }
            set { SetProperty(ref urlFoto, value); }
        }









    }
}
