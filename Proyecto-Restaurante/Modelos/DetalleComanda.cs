using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.Modelos
{
    class DetalleComanda : ObservableObject
    {

        private int cantidad;

        public int Cantidad
        {
            get { return cantidad; }
            set { SetProperty(ref cantidad, value); }
        }

        private DetalleComandaPK _detallesComandaPK;

        public DetalleComandaPK _DetallesComandaPK
        {
            get { return _detallesComandaPK; }
            set { SetProperty(ref _detallesComandaPK, value); }
        }

        private Producto producto;

        public Producto Producto
        {
            get { return producto; }
            set { SetProperty(ref producto, value); }
        }

        public DetalleComanda(int cantidad, DetalleComandaPK detallesComandaPK, Producto producto)
        {
            this.cantidad = cantidad;
            _detallesComandaPK = detallesComandaPK;
            this.producto = producto;
        }

        public DetalleComanda()
        {
        }


    }
}
