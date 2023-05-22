using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.Modelos
{
    class Mesa : ObservableObject
    {
        private int idMesa;
        [JsonProperty("idMesa")]
        public int IdMesa
        {
            get { return idMesa; }
            set { SetProperty(ref idMesa, value); }
        }

        private string nombreMesa;
        [JsonProperty("mesa")]
        public string NombreMesa
        {
            get { return nombreMesa; }
            set { SetProperty(ref nombreMesa, value); }
        }

        private int capacidad;
        [JsonProperty("capacidad")]
        public int Capacidad
        {
            get { return capacidad; }
            set { SetProperty(ref capacidad, value); }
        }

        private bool mesaOcupada;
        [JsonIgnore]
        public bool MesaOcupada
        {
            get { return mesaOcupada; }
            set { SetProperty(ref mesaOcupada, value); }
        }

        private bool capacidadIsOk;
        [JsonIgnore]
        public bool CapacidadIsOk
        {
            get { return capacidadIsOk; }
            set { SetProperty(ref capacidadIsOk, value); }
        }



        public Mesa()
        {
        }

        public Mesa(int idMesa, string mesa, int capacidad)
        {
            this.idMesa = idMesa;
            this.nombreMesa = mesa;
            this.capacidad = capacidad;
        }
    }
}
