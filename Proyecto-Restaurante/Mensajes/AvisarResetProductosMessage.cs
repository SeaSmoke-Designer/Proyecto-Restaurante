using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.Mensajes
{
    class AvisarResetProductosMessage : ValueChangedMessage<bool>
    {
        public AvisarResetProductosMessage(bool b) : base(b) { }
    }
}
