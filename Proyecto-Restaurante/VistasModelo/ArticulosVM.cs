using CommunityToolkit.Mvvm.ComponentModel;
using Proyecto_Restaurante.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.VistasModelo
{
    class ArticulosVM : ObservableObject
    {
        private Producto nuevoProducto;
        public Producto NuevoProducto
        {
            get { return nuevoProducto; }
            set { SetProperty(ref nuevoProducto, value); }
        }

        private Producto productoSeleccionado;
        public Producto ProductoSeleccionado
        {
            get { return productoSeleccionado; }
            set { SetProperty(ref productoSeleccionado, value); }
        }




    }
}
