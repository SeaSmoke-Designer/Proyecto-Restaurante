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
    /// Lógica de interacción para VerificacionAdmin.xaml
    /// </summary>
    public partial class VerificacionAdmin : Window
    {
        private VerificacionAdminVM vm;
        public VerificacionAdmin()
        {
            InitializeComponent();
            vm = new VerificacionAdminVM();
            this.DataContext = vm;
        }

        private void ButtonAceptar_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
