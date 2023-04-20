using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.Modelos
{
    class Producto : ObservableObject
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

        private Categoria idCategoria;

        public Categoria IdCategoria
        {
            get { return idCategoria; }
            set { SetProperty(ref idCategoria, value); }
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

        private string urlFotoProducto;

        public string URLFotoProducto
        {
            get { return urlFotoProducto; }
            set { SetProperty(ref urlFotoProducto, value); }
        }


        public Producto()
        {
        }

        public Producto(int idProducto, string nombreProducto, Categoria categoria, double precioUnitario, int unidadesEnAlmacen, string urlFotoProducto)
        {
            this.idProducto = idProducto;
            this.nombreProducto = nombreProducto;
            this.idCategoria = categoria;
            this.precioUnitario = precioUnitario;
            this.unidadesEnAlmacen = unidadesEnAlmacen;
            this.urlFotoProducto = urlFotoProducto;
        }
    }
}
