using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.Modelos
{
    class DetalleComandaPK : ObservableObject
    {
        private int idComanda;
        [JsonProperty("idComanda")]
        public int IdComanda
        {
            get { return idComanda; }
            set { SetProperty(ref idComanda, value); }
        }

        private int idProducto;
        [JsonProperty("idProducto")]
        public int IdProducto
        {
            get { return idProducto; }
            set { SetProperty(ref idProducto, value); }
        }

        public DetalleComandaPK()
        {
        }

        public DetalleComandaPK(int idComanda, int idProducto)
        {
            this.idComanda = idComanda;
            this.idProducto = idProducto;
        }
    }
}
