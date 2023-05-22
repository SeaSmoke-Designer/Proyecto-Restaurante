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
    /// Lógica de interacción para EditarComanda.xaml
    /// </summary>
    public partial class EditarComanda : Window
    {
        private EditarComandaVM vm;
        public EditarComanda()
        {
            InitializeComponent();
            vm = new EditarComandaVM();
            this.DataContext = vm;
        }

        private void aceptarButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void dataGrid_CurrentCellEndEdit(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellEndEditEventArgs e)
        {
            vm.CalcularImporteTotal();
            vm.DetalleComandaSeleccionada.ActualizarImporteTotal();
        }
    }
}
