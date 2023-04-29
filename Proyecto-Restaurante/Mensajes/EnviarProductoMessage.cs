using CommunityToolkit.Mvvm.Messaging.Messages;
using Proyecto_Restaurante.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.Mensajes
{
    class EnviarProductoMessage : RequestMessage<Producto> { }
}
