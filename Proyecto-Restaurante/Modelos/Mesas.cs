using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.Modelos
{
    class Mesas : ObservableObject
    {
        private int idMesa;

        public int IdMesa
        {
            get { return idMesa; }
            set { SetProperty(ref idMesa, value); }
        }

        private string mesa;

        public string Mesa
        {
            get { return mesa; }
            set { SetProperty(ref mesa, value); }
        }

        private int capacidad;

        public int Capacidad
        {
            get { return capacidad; }
            set { SetProperty(ref capacidad, value); }
        }

        public Mesas()
        {
        }

        public Mesas(int idMesa, string mesa, int capacidad)
        {
            this.idMesa = idMesa;
            this.mesa = mesa;
            this.capacidad = capacidad;
        }
    }
}
