using Proyecto_Restaurante.VistasModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Proyecto_Restaurante.Vistas
{
    /// <summary>
    /// Lógica de interacción para AñadirEditarEmpleado.xaml
    /// </summary>
    public partial class AñadirEditarEmpleado : Window
    {
        private AñadirEditarEmpleadoVM vm;
        public AñadirEditarEmpleado()
        {
            InitializeComponent();
            vm = new AñadirEditarEmpleadoVM();
            this.DataContext = vm;
        }
    }
}
