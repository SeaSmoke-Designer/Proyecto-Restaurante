using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Proyecto_Restaurante.Mensajes;
using Proyecto_Restaurante.Modelos;
using Proyecto_Restaurante.Servicios;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.VistasModelo
{
    class AñadirEditarProductoVM : ObservableObject
    {
        private readonly string imagenDefault = "../Assets/Add_Image.png";
        public RelayCommand GuardarProductoCommand { get; }
        public RelayCommand NuevaImagenProductoCommand { get; }

        private readonly ServicioDialogo servicioDialogo;
        private readonly ServicioAzure servicioAzure;
        private readonly ServicioAPIRestRestaurante servicioAPIRestRestaurante;
        
        private Producto productoActual;

        public Producto ProductoActual
        {
            get { return productoActual; }
            set { SetProperty(ref productoActual, value); }
        }

        private ObservableCollection<Categoria> listaCategorias;

        public ObservableCollection<Categoria> ListaCategorias
        {
            get { return listaCategorias; }
            set { SetProperty(ref listaCategorias, value); }
        }

        private Categoria categoriaSeleccionada;

        public Categoria CategoriaSeleccionada
        {
            get { return categoriaSeleccionada; }
            set { SetProperty(ref categoriaSeleccionada, value); }
        }

        private string modoVentana;

        public string ModoVentana
        {
            get { return modoVentana; }
            set { SetProperty(ref modoVentana, value); }
        }


        public AñadirEditarProductoVM()
        {
            ProductoActual = WeakReferenceMessenger.Default.Send<EnviarProductoMessage>();
            if(ProductoActual is null)
            {
                ProductoActual = new Producto();
                ProductoActual.URLFotoProducto = imagenDefault;
            }
            ModoVentana = ProductoActual.IdProducto == 0 ? "Crear Producto" : "Editar Producto";
            NuevaImagenProductoCommand = new RelayCommand(SeleccionImagen);
            GuardarProductoCommand = new RelayCommand(GuardarProducto);
            servicioAzure = new ServicioAzure();
            servicioDialogo = new ServicioDialogo();
            servicioAPIRestRestaurante = new ServicioAPIRestRestaurante();
            CargarCategorias();
        }

        public void SeleccionImagen()
        {
            string file = servicioDialogo.DialogoAbrirFichero();
            ProductoActual.URLFotoProducto = file != null ? servicioAzure.AlmacenarImagenEnLaNube(file) : imagenDefault;
        }

        public void GuardarProducto()
        {
            if(ProductoActual.IdProducto == 0)
            {
                AñadirNuevoProducto();
            }
            else
            {

            }
        }

        public void AñadirNuevoProducto()
        {
            if (!ProductoExiste())
            {
                if (ProductoActual.IdCategoria != null)
                {
                    if(ProductoActual.URLFotoProducto.Equals(imagenDefault) || ProductoActual.URLFotoProducto == null)
                    {
                        ProductoActual.URLFotoProducto = "../Assets/SinImagen.png";
                    }
                    
                    IRestResponse response = servicioAPIRestRestaurante.PostProducto(ProductoActual);
                    if(response.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        
                        WeakReferenceMessenger.Default.Send(new EnviarNuevoProductoMessage(ProductoActual));
                    }else if(response.StatusCode == System.Net.HttpStatusCode.UnsupportedMediaType)
                    {
                        servicioDialogo.MostrarMensajeError("Error al agregar el producto", "Error ");
                    }
                }
                else
                {
                    servicioDialogo.MostrarMensajeAdvertencia("Eliga una categoria para el producto", "Prodcuto sin categoria");
                }
            }
            else
            {
                servicioDialogo.MostrarMensajeAdvertencia("No se puede repetir los productos, cambie el nombre", "Producto repetido");
            }
        }

        public bool ProductoExiste()
        {
            ObservableCollection<Producto> listaProductos = servicioAPIRestRestaurante.GetProductos();
            foreach (Producto item in listaProductos)
            {
                if (ProductoActual.NombreProducto.Equals(item.NombreProducto))
                {
                    return true;
                }
            }
            return false;
        }

        public void CargarCategorias()
        {
            ListaCategorias = servicioAPIRestRestaurante.GetCategorias();
        }
    }
}
