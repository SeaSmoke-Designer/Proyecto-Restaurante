using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.Mensajes
{
    class EnviarValidacionAdminMessage : ValueChangedMessage<bool>
    {
        public EnviarValidacionAdminMessage(bool validacion) : base(validacion) { }
    }
}
