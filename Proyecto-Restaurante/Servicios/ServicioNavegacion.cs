﻿using Proyecto_Restaurante.Vistas;
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
    }
}
