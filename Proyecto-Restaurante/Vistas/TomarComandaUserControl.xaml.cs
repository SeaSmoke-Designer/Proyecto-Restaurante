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
    /// Lógica de interacción para TomarComandaUserControl.xaml
    /// </summary>
    public partial class TomarComandaUserControl : UserControl
    {
        private TomarComandaVM vm;
        public TomarComandaUserControl()
        {
            
            InitializeComponent();
            vm = new TomarComandaVM();
            this.DataContext = vm;
        }

        /*private void dataGrid_CurrentCellEndEdit(object sender, Syncfusion.UI.Xaml.Grid.CurrentCellEndEditEventArgs e)
        {
            vm.CalcularImporteTotalComanda();
            vm.DetalleComandaSeleccionada.ActualizarImporteTotal();
        }*/
    }
}
