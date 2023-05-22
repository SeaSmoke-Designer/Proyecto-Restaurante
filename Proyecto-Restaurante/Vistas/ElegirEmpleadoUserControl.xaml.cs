﻿using Proyecto_Restaurante.VistasModelo;
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
    /// Lógica de interacción para ElegirEmpleadoUserControl.xaml
    /// </summary>
    public partial class ElegirEmpleadoUserControl : UserControl
    {
        private ElegirEmpleadoVM vm;
        public ElegirEmpleadoUserControl()
        {
            InitializeComponent();
            vm = new ElegirEmpleadoVM();
            this.DataContext = vm;
        }
    }
}