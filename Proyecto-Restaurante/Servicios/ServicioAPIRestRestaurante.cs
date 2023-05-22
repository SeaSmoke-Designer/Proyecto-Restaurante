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
        private readonly string clave;
        
        
        public ServicioAPIRestRestaurante()
        {
            this.clave = Properties.Settings.Default.ClaveAPIRest;
        }

        public ObservableCollection<Categoria> GetCategorias()
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest("categorias",Method.GET);
            request.AddHeader("Root",clave);
            var response = cliente.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<Categoria>>(response.Content);
        }

        public ObservableCollection<Producto> GetProductos()
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest("productos", Method.GET);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
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

        public IRestResponse PutProducto(Producto editarProducto)
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest("productos", Method.PUT);
            string data = JsonConvert.SerializeObject(editarProducto);
            request.AddParameter("application/json", data, ParameterType.RequestBody);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
            return response;
        }

        public IRestResponse DeleteProducto(int id)
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest($"productos/{id}", Method.DELETE);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
            return response;
        }

        public ObservableCollection<Empleado> GetEmpleados()
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest("empleados", Method.GET);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<Empleado>>(response.Content);
        }

        public Empleado GetEmpleado(int id)
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest($"empleados/{id}", Method.GET);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
            return JsonConvert.DeserializeObject<Empleado>(response.Content);
        }

        public Empleado GetEmpleadoByDni(string dni)
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest($"empleados/dni/{dni}", Method.GET);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
            return JsonConvert.DeserializeObject<Empleado>(response.Content);
        }

        public IRestResponse PostEmpleado(Empleado nuevoEmpleado)
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest("empleados", Method.POST);
            string data = JsonConvert.SerializeObject(nuevoEmpleado);
            request.AddParameter("application/json", data, ParameterType.RequestBody);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
            return response;
        }

        public IRestResponse PutEmpleado(Empleado editarEmpleado)
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest("empleados", Method.PUT);
            string data = JsonConvert.SerializeObject(editarEmpleado);
            request.AddParameter("application/json", data, ParameterType.RequestBody);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
            return response;
        }

        public IRestResponse DeleteEmpleado(int id)
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest($"empleados/{id}", Method.DELETE);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
            return response;
        }

        public ObservableCollection<Comanda> GetComandas()
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest("comandas", Method.GET);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<Comanda>>(response.Content);
        }

        public ObservableCollection<Comanda> GetComandasPagadas(bool pagada)
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest($"comandas/pagadas/{pagada}", Method.GET);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<Comanda>>(response.Content);
        }

        public Comanda GetComanda(int id)
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest($"comandas/{id}", Method.GET);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
            return JsonConvert.DeserializeObject<Comanda>(response.Content);
        }


        public IRestResponse PostComanda(Comanda nuevaComanda)
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest("comandas", Method.POST);
            string data = JsonConvert.SerializeObject(nuevaComanda);
            request.AddParameter("application/json", data, ParameterType.RequestBody);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
            return response;
        }

        public IRestResponse PutComanda(Comanda editarComanda)
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest("comandas", Method.PUT);
            string data = JsonConvert.SerializeObject(editarComanda);
            request.AddParameter("application/json", data, ParameterType.RequestBody);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
            return response;
        }

        public IRestResponse DeleteComanda(int id)
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest($"comandas/{id}", Method.DELETE);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
            return response;
        }

        public ObservableCollection<Mesa> GetMesas()
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest("mesas", Method.GET);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<Mesa>>(response.Content);
        }

        public Mesa GetMesa(int id)
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest($"mesas/{id}", Method.GET);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
            return JsonConvert.DeserializeObject<Mesa>(response.Content);
        }

        public IRestResponse PostMesa(Mesa nuevaMesa)
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest("mesas", Method.POST);
            string data = JsonConvert.SerializeObject(nuevaMesa);
            request.AddParameter("application/json", data, ParameterType.RequestBody);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
            return response;
        }

        public IRestResponse PutMesa(Mesa editarMesa)
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest("mesas", Method.PUT);
            string data = JsonConvert.SerializeObject(editarMesa);
            request.AddParameter("application/json", data, ParameterType.RequestBody);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
            return response;
        }

        public IRestResponse DeleteMesa(int id)
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest($"mesas/{id}", Method.DELETE);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
            return response;
        }

        public ObservableCollection<DetalleComanda> GetDetallesComanda()
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest("detallescomanda", Method.GET);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<DetalleComanda>>(response.Content);
        }

        public IRestResponse PostDetallesComanda(DetalleComanda detalleComandaNuevo)
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest("detallescomanda", Method.POST);
            string data = JsonConvert.SerializeObject(detalleComandaNuevo);
            request.AddParameter("application/json", data, ParameterType.RequestBody);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
            return response;
        }

        public IRestResponse PutDetallesComanda(DetalleComanda editarDetalleComanda)
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest("detallescomanda", Method.PUT);
            string data = JsonConvert.SerializeObject(editarDetalleComanda);
            request.AddParameter("application/json", data, ParameterType.RequestBody);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
            return response;
        }

        public IRestResponse DeleteDetallesComanda(int id, int id2)
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest($"detallescomanda/{id}/{id2}", Method.DELETE);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
            return response;
        }
    }
}
