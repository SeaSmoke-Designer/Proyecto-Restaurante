using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using Proyecto_Restaurante.Convertidores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Proyecto_Restaurante.Modelos
{
    class Empleado : ObservableObject
    {
        private int idEmpleado;
        [JsonProperty("idEmpleado")]
        public int IdEmpleado
        {
            get { return idEmpleado; }
            set { SetProperty(ref idEmpleado, value); }
        }

        private string dni;
        [JsonProperty("dni")]
        public string Dni
        {
            get { return dni; }
            set { SetProperty(ref dni, value); }
        }

        private string nombre;
        [JsonProperty("nombre")]
        public string Nombre
        {
            get { return nombre; }
            set { SetProperty(ref nombre, value); }
        }


        private string apellido;
        [JsonProperty("apellido")]
        public string Apellido
        {
            get { return apellido; }
            set { SetProperty(ref apellido, value); }
        }


        private string cargo;
        [JsonProperty("cargo")]
        public string Cargo
        {
            get { return cargo; }
            set { SetProperty(ref cargo, value); }
        }

        private string fechaNacimiento;
        [JsonProperty("fechaNacimiento")]
        public string FechaNacimiento
        {
            get { return fechaNacimiento; }
            set { SetProperty(ref fechaNacimiento, value); }
        }

        private string direccion;
        [JsonProperty("direccion")]
        public string Direccion
        {
            get { return direccion; }
            set { SetProperty(ref direccion, value); }
        }

        private string telefonoParticular;
        [JsonProperty("telefonoParticular")]
        public string TelefonoParticular
        {
            get { return telefonoParticular; }
            set { SetProperty(ref telefonoParticular, value); }
        }

        private string urlFoto;
        [JsonProperty("URLFoto")]
        public string URLFoto
        {
            get { return urlFoto; }
            set { SetProperty(ref urlFoto, value); }
        }

        private string contraseñaEmpleado;
        [JsonProperty("contraseñaEmpleado")]
        public string ContraseñaEmpleado
        {
            get { return contraseñaEmpleado; }
            set { SetProperty(ref contraseñaEmpleado, value); }
        }


        public Empleado()
        {
        }

       
    }
}
