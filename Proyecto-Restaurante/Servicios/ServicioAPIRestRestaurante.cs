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

        /// <summary>
        /// Este metodo simplemete se utiliza para cambiar el nombre de las propiedades para que la API lo acepte sin ningun problema
        /// </summary>
        /// <param name="producto"></param>
        /// <returns></returns>
        
        private object CambiarPropiedadesProducto(Producto producto)
        {
            return new
            {
                idProducto = producto.IdProducto,
                nombreProducto = producto.NombreProducto,
                precioUnitario = producto.PrecioUnitario,
                unidadesEnAlmacen = producto.UnidadesEnAlmacen,
                URLFotoProducto = producto.URLFotoProducto,
                idCategoria = new
                {
                    descripcion = producto.IdCategoria.Descripcion,
                    idCategoria = producto.IdCategoria.IdCategoria,
                    nombreCategoria = producto.IdCategoria.NombreCategoria,
                    uRLFotoCategoria = producto.IdCategoria.URLFotoCategoria
                }
            };
        }

        private object CambiarPropiedadesEmpleado(Empleado empleado)
        {
            return new
            {
                idEmpleado = empleado.IdEmpleado,
                dni = empleado.Dni,
                nombre = empleado.Nombre,
                apellido = empleado.Apellido,
                cargo = empleado.Cargo,
                URLFoto = empleado.URLFoto,
                fechaNacimiento = empleado.FechaNacimiento,
                direccion = empleado.Direccion,
                telefonoParticular = empleado.TelefonoParticular,
                contraseñaEmpleado = empleado.ContraseñaEmpleado
            };
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
    }
}
