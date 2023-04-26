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
        private string clave;
        
        
        public ServicioAPIRestRestaurante()
        {
            this.clave = Properties.Settings.Default.ClaveAPIRest;
        }

        

        public ObservableCollection<Categoria> GetCategorias()
        {
            //clave = Properties.Settings.Default.ClaveAPIRest;
            var cliente = new RestClient(Properties.Settings.Default.endpointLocal);
            var cliente2 = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest("categorias",Method.GET);
            request.AddHeader("Root",clave);
            //request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Content-Type",)
            var response = cliente2.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<Categoria>>(response.Content);
        }

        public ObservableCollection<Producto> GetProductos()
        {
            //clave = Properties.Settings.Default.ClaveAPIRest;
            var cliente = new RestClient(Properties.Settings.Default.endpointLocal);
            var cliente2 = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest("productos", Method.GET);
            request.AddHeader("Root", clave);
            //request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Content-Type",)
            var response = cliente2.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<Producto>>(response.Content);
        }

        public IRestResponse PostProducto(Producto nuevoProducto)
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest("productos", Method.POST);
            string data = JsonConvert.SerializeObject(nuevoProducto);
            request.AddParameter("application/json", data, ParameterType.RequestBody);
            request.AddHeader("Root", clave);
            
            var response = cliente.Execute(request);
            return response;
        }
    }
}
