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
using System.Windows;

namespace Proyecto_Restaurante.VistasModelo
{
    class GestionarMesasVM : ObservableObject
    {

        //private readonly ServicioNavegacion servicioNavegacion;
        private readonly ServicioAPIRestRestaurante servicioAPIRestRestaurante;
        private readonly ServicioDialogo servicioDialogo;

        public RelayCommand AñadirNuevaMesaCommand { get; }
        public RelayCommand GuardarCambiosCommand { get; }
        public RelayCommand EliminarMesaCommand { get; }

        private Mesa mesaSeleccionada;

        public Mesa MesaSeleccionada
        {
            get { return mesaSeleccionada; }
            set { SetProperty(ref mesaSeleccionada, value); }
        }

        private ObservableCollection<Mesa> listaMesas;

        public ObservableCollection<Mesa> ListaMesas
        {
            get { return listaMesas; }
            set { SetProperty(ref listaMesas, value); }
        }

        public GestionarMesasVM()
        {
            servicioAPIRestRestaurante = new ServicioAPIRestRestaurante();
            servicioDialogo = new ServicioDialogo();
            
            CargarMesas();
            EliminarMesaCommand = new RelayCommand(EliminarMesa);
            AñadirNuevaMesaCommand = new RelayCommand(AñadirNuevaMesa);
            GuardarCambiosCommand = new RelayCommand(GuardarCambios);
        }

        public void CargarMesas()
        {
            ListaMesas = servicioAPIRestRestaurante.GetMesas();
        }

        public void GuardarCambios()
        {
            //ObservableCollection<Mesa> listaAux = servicioAPIRestRestaurante.GetMesas();
            if(ComprobarCambios())
            {
                MessageBox.Show("Todo lindo");
            }

            //ListaMesas.CollectionChanged
        }

        public bool ComprobarCambios()
        {
            ObservableCollection<Mesa> listaAux = servicioAPIRestRestaurante.GetMesas();
            if (listaAux.Count != ListaMesas.Count)
                return true;


            //ObservableCollection<Mesa> listaAux = servicioAPIRestRestaurante.GetMesas();
            for (int i = 0; i < listaAux.Count; i++)
            {
                //listaAux.
            }

            foreach (Mesa item in listaAux)
            {

            }

            return false;
        }

        public void AñadirNuevaMesa()
        {
            ListaMesas.Add(new Mesa());
        }

        

        public void EliminarMesa()
        {
            MessageBoxResult result = MessageBox.Show("¿Estas seguro de eliminar esta mesa?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                if (MesaSeleccionada != null)
                {
                    if (ExisteMesaBD())
                    {

                        if (!ComprobarMesaConComanda())
                        {
                            IRestResponse response = servicioAPIRestRestaurante.DeleteMesa(MesaSeleccionada.IdMesa);
                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                ListaMesas.Remove(MesaSeleccionada);
                                servicioDialogo.MostrarMensajeInformacion("Mesa eliminada con exito!", "MESA ELIMINADA");
                            }
                            else
                            {
                                servicioDialogo.MostrarMensajeError(response.Content, "Error al borrar la mesa");
                            }
                        }
                        else
                        {
                            servicioDialogo.MostrarMensajeError("No se puede eliminar la mesa seleccionada porque tiene una comanda asignada", "NO SE PUEDE ELIMINAR LA MESA");
                        }
                    }
                    else
                    {
                        ListaMesas.Remove(MesaSeleccionada);
                    }
                }
            }
        }

        public bool ComprobarMesaConComanda()
        {
            ObservableCollection<Comanda> listaComandas = servicioAPIRestRestaurante.GetComandas();
            foreach (Comanda item in listaComandas)
            {
                if (item.Mesa.IdMesa == MesaSeleccionada.IdMesa)
                    return true;
            }

            return false;
        }

        public bool ExisteMesaBD()
        {
            
            if (MesaSeleccionada.IdMesa == 0 || MesaSeleccionada.IdMesa == null)
                return false;

            /*ObservableCollection<Mesa> listaAux = servicioAPIRestRestaurante.GetMesas();
            foreach (Mesa item in listaAux)
            {
                if (item.IdMesa == MesaSeleccionada.IdMesa)
                    return true;
            }*/

            return true;
        }
    }
}
