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
    /// Lógica de interacción para VentanaCobrarComanda.xaml
    /// </summary>
    public partial class VentanaCobrarComanda : Window
    {
        private VentanaCobrarComandaVM vm;
        public VentanaCobrarComanda()
        {
            InitializeComponent();
            vm = new VentanaCobrarComandaVM();
            this.DataContext = vm;
        }

        private void aceptarButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
