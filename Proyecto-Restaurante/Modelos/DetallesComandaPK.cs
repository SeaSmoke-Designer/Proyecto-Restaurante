using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.Modelos
{
    class DetallesComandaPK : ObservableObject
    {
        private int idComanda;

        public int IdComanda
        {
            get { return idComanda; }
            set { SetProperty(ref idComanda, value); }
        }

        private int idProducto;

        public int IdProducto
        {
            get { return idProducto; }
            set { SetProperty(ref idProducto, value); }
        }

        public DetallesComandaPK()
        {
        }

        public DetallesComandaPK(int idComanda, int idProducto)
        {
            this.idComanda = idComanda;
            this.idProducto = idProducto;
        }
    }
}
