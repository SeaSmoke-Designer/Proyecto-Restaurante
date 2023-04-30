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
            //var cliente = new RestClient(Properties.Settings.Default.endpointLocal);
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest("categorias",Method.GET);
            request.AddHeader("Root",clave);
            //request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Content-Type",)
            var response = cliente.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<Categoria>>(response.Content);
        }

        public ObservableCollection<Producto> GetProductos()
        {
            //clave = Properties.Settings.Default.ClaveAPIRest;
            //var cliente = new RestClient(Properties.Settings.Default.endpointLocal);
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest("productos", Method.GET);
            request.AddHeader("Root", clave);
            //request.AddHeader("Content-Type", "application/json");
            //request.AddHeader("Content-Type",)
            var response = cliente.Execute(request);
            return JsonConvert.DeserializeObject<ObservableCollection<Producto>>(response.Content);
        }

        public IRestResponse PostProducto(Producto nuevoProducto)
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest("productos", Method.POST);
            string data = JsonConvert.SerializeObject(new
            {
                idProducto = nuevoProducto.IdProducto,
                nombreProducto = nuevoProducto.NombreProducto,
                precioUnitario = nuevoProducto.PrecioUnitario,
                unidadesEnAlmacen = nuevoProducto.UnidadesEnAlmacen,
                URLFotoProducto = nuevoProducto.URLFotoProducto,
                idCategoria = new
                {
                    descripcion = nuevoProducto.IdCategoria.Descripcion,
                    idCategoria = nuevoProducto.IdCategoria.IdCategoria,
                    nombreCategoria = nuevoProducto.IdCategoria.NombreCategoria,
                    uRLFotoCategoria = nuevoProducto.IdCategoria.URLFotoCategoria
                }
            });
            request.AddParameter("application/json", data, ParameterType.RequestBody);
            request.AddHeader("Root", clave);
            var response = cliente.Execute(request);
            return response;
        }

        public IRestResponse PutProducto(Producto editarProducto)
        {
            var cliente = new RestClient(Properties.Settings.Default.endpointAzure);
            RestRequest request = new RestRequest("productos", Method.PUT);
            string data = JsonConvert.SerializeObject(new
            {
                idProducto = editarProducto.IdProducto,
                nombreProducto = editarProducto.NombreProducto,
                precioUnitario = editarProducto.PrecioUnitario,
                unidadesEnAlmacen = editarProducto.UnidadesEnAlmacen,
                URLFotoProducto = editarProducto.URLFotoProducto,
                idCategoria = new
                {
                    descripcion = editarProducto.IdCategoria.Descripcion,
                    idCategoria = editarProducto.IdCategoria.IdCategoria,
                    nombreCategoria = editarProducto.IdCategoria.NombreCategoria,
                    uRLFotoCategoria = editarProducto.IdCategoria.URLFotoCategoria
                }
            });
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
    }
}
