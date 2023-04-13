using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.Modelos
{
    class Categoria : ObservableObject
    {
        private int idCategoria;

        public int IdCategoria
        {
            get { return idCategoria; }
            set { SetProperty(ref idCategoria, value); }
        }

        private string nombreCategoria;

        public string NombreCategoria 
        {
            get { return nombreCategoria; }
            set { SetProperty(ref nombreCategoria, value); }
        }

        private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { SetProperty(ref descripcion, value); }
        }

        private string urlFotoCategoria;

        public string URLFotoCategoria
        {
            get { return urlFotoCategoria; }
            set { SetProperty(ref urlFotoCategoria, value); }
        }

        public Categoria()
        {
        }

        public Categoria(int idCategoria, string nombreCategoria, string descripcion, string urlFotoCategoria)
        {
            this.idCategoria = idCategoria;
            this.nombreCategoria = nombreCategoria;
            this.descripcion = descripcion;
            this.urlFotoCategoria = urlFotoCategoria;
        }
    }
}
