using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Proyecto_Restaurante.Modelos;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using System.Threading.Tasks;


namespace Proyecto_Restaurante.Servicios
{
    class ServicioAPIRestRestaurante
    {
        public ServicioAPIRestRestaurante()
        {
        }

        public ObservableCollection<Categoria> GetCategorias()
        {
            string clave = Properties.Settings.Default.ClaveAPIRest;
            var cliente = new RestClient(Properties.Settings.Default.endpoint);
            RestRequest request = new RestRequest("categorias",Method.Get);
            request.AddHeader("Root",clave);
            //request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Content-Type",)
            var response = cliente.Execute(request);

            



            return JsonConvert.DeserializeObject<ObservableCollection<Categoria>>(response.Content);
        }
    }
}
