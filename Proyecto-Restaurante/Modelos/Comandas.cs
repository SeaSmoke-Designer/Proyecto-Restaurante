using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.Modelos
{
    class Comandas: ObservableObject
    {
        private int idComanda;

        public int IdComanda
        {
            get { return idComanda; }
            set { SetProperty(ref idComanda, value); }
        }

        private Empleados empleado;

        public Empleados Empleado
        {
            get { return empleado; }
            set { SetProperty(ref empleado, value); }
        }

        private Mesas mesa;

        public Mesas Mesa
        {
            get { return mesa; }
            set { SetProperty(ref mesa, value); }
        }

        private int cantidadPersonas;

        public int CantidadPersonas
        {
            get { return cantidadPersonas; }
            set { SetProperty(ref cantidadPersonas, value); }
        }

        private bool pagada;

        public bool Pagada
        {
            get { return pagada; }
            set { SetProperty(ref pagada, value); }
        }

        private DateTime fecha;

        public DateTime Fecha
        {
            get { return fecha; }
            set { SetProperty(ref fecha, value); }
        }




    }
}
