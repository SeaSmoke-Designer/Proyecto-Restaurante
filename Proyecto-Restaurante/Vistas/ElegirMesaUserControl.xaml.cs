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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Proyecto_Restaurante.Vistas
{
    /// <summary>
    /// Lógica de interacción para ElegirMesaUserControl.xaml
    /// </summary>
    public partial class ElegirMesaUserControl : UserControl
    {
        private ElegirMesaVM vm;
        public ElegirMesaUserControl()
        {
            InitializeComponent();
            vm = new ElegirMesaVM();
            this.DataContext = vm;
        }
    }
}
