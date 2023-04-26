using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
    class AñadirNuevoProductoVM : ObservableObject
    {
        private string imagenDefault = "../Assets/Add_Image.png";
        public RelayCommand AgregarProductoCommand { get; }
        public RelayCommand NuevaImagenProductoCommand { get; }

        private readonly ServicioDialogo servicioDialogo;
        private readonly ServicioAzure servicioAzure;
        private readonly ServicioAPIRestRestaurante servicioAPIRestRestaurante;
        
        private Producto nuevoProducto;

        public Producto NuevoProducto
        {
            get { return nuevoProducto; }
            set { SetProperty(ref nuevoProducto, value); }
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

        public AñadirNuevoProductoVM()
        {
            NuevoProducto = new Producto();
            NuevoProducto.URLFotoProducto = imagenDefault;
            NuevaImagenProductoCommand = new RelayCommand(SeleccionImagen);
            AgregarProductoCommand = new RelayCommand(AñadirNuevoProducto);
            servicioAzure = new ServicioAzure();
            servicioDialogo = new ServicioDialogo();
            servicioAPIRestRestaurante = new ServicioAPIRestRestaurante();
            CargarCategorias();
        }

        public void SeleccionImagen()
        {
            string file = servicioDialogo.DialogoAbrirFichero();
            NuevoProducto.URLFotoProducto = file != null ? servicioAzure.AlmacenarImagenEnLaNube(file) : "../Assets/Add_Image.png";
        }

        public void AñadirNuevoProducto()
        {
            if (!ProductoExiste())
            {
                if (NuevoProducto.IdCategoria != null)
                {
                    if(NuevoProducto.URLFotoProducto.Equals(imagenDefault) || NuevoProducto.URLFotoProducto == null)
                    {
                        NuevoProducto.URLFotoProducto = "../Assets/SinImagen.png";
                    }
                    //NuevoProducto.IdProducto = 0;
                    IRestResponse response = servicioAPIRestRestaurante.PostProducto(NuevoProducto);
                    if(response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        servicioDialogo.MostrarMensajeInformacion("El producto se ha agregado con exito", "Producto añadido");
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
                if (NuevoProducto.NombreProducto.Equals(item.NombreProducto))
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
