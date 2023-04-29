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
    /// Lógica de interacción para AñadirNuevoProducto.xaml
    /// </summary>
    public partial class AñadirNuevoProducto : Window
    {
        private AñadirEditarProductoVM vm;
        public AñadirNuevoProducto()
        {
            InitializeComponent();
            vm = new AñadirEditarProductoVM();
            this.DataContext = vm;
        }

        private void ButtonClick_Aceptar(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

       
    }
}
