using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Proyecto_Restaurante.Servicios
{
    class ServicioDialogo
    {
        public string DialogoAbrirFichero()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : null;
        }
        public void MostrarMensajePersonalizdo(string mensaje, string titulo, MessageBoxButton button, MessageBoxImage image)
        {
            MessageBox.Show(mensaje, titulo, button, image);
        }

        public void MostrarMensajeAdvertencia(string mensaje, string titulo)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public void MostrarMensajeError(string mensaje, string titulo)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void MostrarMensajeInformacion(string mensaje, string titulo)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
