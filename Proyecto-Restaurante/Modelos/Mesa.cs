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

        private string _mesa;
        [JsonProperty("mesa")]
        public string _Mesa
        {
            get { return _mesa; }
            set { SetProperty(ref _mesa, value); }
        }

        private int capacidad;
        [JsonProperty("capacidad")]
        public int Capacidad
        {
            get { return capacidad; }
            set { SetProperty(ref capacidad, value); }
        }

        public Mesa()
        {
        }

        public Mesa(int idMesa, string mesa, int capacidad)
        {
            this.idMesa = idMesa;
            this._mesa = mesa;
            this.capacidad = capacidad;
        }
    }
}
