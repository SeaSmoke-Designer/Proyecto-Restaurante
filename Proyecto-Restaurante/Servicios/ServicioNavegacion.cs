using Proyecto_Restaurante.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Proyecto_Restaurante.Servicios
{
    class ServicioNavegacion
    {
        public ServicioNavegacion()
        {
        }

        public UserControl CargarArticulos()
        {
            return new ArticulosUserControl();
        }

        public bool? CargarAñadirEditarProducto()
        {
            AñadirNuevoProducto añadirNuevoProducto = new AñadirNuevoProducto();
            return añadirNuevoProducto.ShowDialog();
        }

        public UserControl CargarGestionarEmpleados()
        {
            return new GestionarEmpleadosUserControl();
        }

        public bool? CargarAñadirEditarEmpleado()
        {
            AñadirEditarEmpleado añadirEditarEmpleado = new AñadirEditarEmpleado();
            return añadirEditarEmpleado.ShowDialog();
        }

        public bool? CargarValidacionAdmin()
        {
            VerificacionAdmin verificacionAdmin = new VerificacionAdmin();
            return verificacionAdmin.ShowDialog();
        }

        public UserControl CargarHistoricoComandas()
        {
            return new HistoricoComandasUserControl();
        }

        public bool? CargarVerDetallesComanda()
        {
            VerDetallesComanda verDetallesComanda = new VerDetallesComanda();
            return verDetallesComanda.ShowDialog();
        }

        public UserControl CargarGestionarMesas()
        {
            return new GestionarMesasUserControl();
        }

        
    }
}
