using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.Modelos
{
    class DetallesComanda : ObservableObject
    {

        private int cantidad;

        public int Cantidad
        {
            get { return cantidad; }
            set { SetProperty(ref cantidad, value); }
        }

        private DetallesComandaPK _detallesComandaPK;

        public DetallesComandaPK _DetallesComandaPK
        {
            get { return _detallesComandaPK; }
            set { SetProperty(ref _detallesComandaPK, value); }
        }

        private Productos producto;

        public Productos Producto
        {
            get { return producto; }
            set { SetProperty(ref producto, value); }
        }

        public DetallesComanda(int cantidad, DetallesComandaPK detallesComandaPK, Productos producto)
        {
            this.cantidad = cantidad;
            _detallesComandaPK = detallesComandaPK;
            this.producto = producto;
        }

        public DetallesComanda()
        {
        }


    }
}
