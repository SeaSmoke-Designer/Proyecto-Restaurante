using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Proyecto_Restaurante.Convertidores
{
    class ConvertidorFechaEmpleado : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string fecha = value.ToString();

                if (fecha != null)
                {
                    string[] found = fecha.Split('T');
                    return found[0];
                }
                else return "";
            }
            else return "";
            

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            string result = value.ToString();
            
            if(result is null)
            {
                return "";
            }
            else
            {
                return result;
            }
        }
    }
}
