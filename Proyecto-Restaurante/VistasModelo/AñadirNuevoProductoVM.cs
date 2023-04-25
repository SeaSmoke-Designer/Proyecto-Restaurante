using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Proyecto_Restaurante.Modelos;
using Proyecto_Restaurante.Servicios;
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
            NuevoProducto.URLFotoProducto = "../Assets/Add_Image.png";
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
            if (ProductoExiste())
            {
                servicioDialogo.MostrarMensajeInformacion("El producto existe", "Existe producto");
            }
            else
            {
                servicioDialogo.MostrarMensajeInformacion("El producto no existe", "No existe producto");
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
