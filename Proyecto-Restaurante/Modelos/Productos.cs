using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.Modelos
{
    class Productos : ObservableObject
    {
        private int idProducto;

        public int IdProducto
        {
            get { return idProducto; }
            set { SetProperty(ref idProducto, value); }
        }

        private string nombreProducto;

        public string NombreProducto
        {
            get { return nombreProducto; }
            set { SetProperty(ref nombreProducto, value); }
        }

        private Categorias _categoria;

        public Categorias _Categoria
        {
            get { return _categoria; }
            set { SetProperty(ref _categoria, value); }
        }

        private double precioUnitario;

        public double PrecioUnitario
        {
            get { return precioUnitario; }
            set { SetProperty(ref precioUnitario, value); }
        }

        private int unidadesEnAlmacen;

        public int UnidadesEnAlmacen
        {
            get { return unidadesEnAlmacen; }
            set { SetProperty(ref unidadesEnAlmacen, value); }
        }





    }
}
