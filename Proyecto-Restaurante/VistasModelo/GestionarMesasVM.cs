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

        private ObservableCollection<Mesa> listaPOST;

        public ObservableCollection<Mesa> ListaPOST
        {
            get { return listaPOST; }
            set { SetProperty(ref listaPOST, value); }
        }

        private ObservableCollection<Mesa> listaPUT;

        public ObservableCollection<Mesa> ListaPUT
        {
            get { return listaPUT; }
            set { SetProperty(ref listaPUT, value); }
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
            ListaPOST = new ObservableCollection<Mesa>();
            ListaPUT = new ObservableCollection<Mesa>();
            IRestResponse responsePOST = null;
            IRestResponse responsePUT = null;
            StringBuilder sb = new StringBuilder();
            if (ComprobarCambiosPOST())
            {
                if (ListaPOST.Count != 0)
                {
                    sb.Append("Mesas creadas: \n");
                    foreach (Mesa item in ListaPOST)
                    {
                        if (item.Capacidad != 0)
                        {
                            responsePOST = servicioAPIRestRestaurante.PostMesa(item);
                            if (responsePOST.StatusCode == System.Net.HttpStatusCode.Created)
                                sb.Append($"- {item.NombreMesa}\n");
                            else if (responsePOST.StatusCode == System.Net.HttpStatusCode.UnsupportedMediaType)
                                sb.Append($"Error al crear la nueva {item.NombreMesa}\n");
                        }
                        else
                        {
                            servicioDialogo.MostrarMensajeAdvertencia($"La {item.NombreMesa} no puede tener la capacidad a 0", "ADVERTENCIA - MESA SIN CAPACIDAD");
                        }
                    }
                }
            }

            ComprobarCambiosPUT();

            if (ListaPUT.Count != 0)
            {
                sb.Append("\nMesas editadas: \n");
                foreach (Mesa item in ListaPUT)
                {
                    responsePUT = servicioAPIRestRestaurante.PutMesa(item);
                    if (responsePUT.StatusCode == System.Net.HttpStatusCode.OK)
                        sb.Append($"- {item.NombreMesa}\n");
                    else if (responsePUT.StatusCode == System.Net.HttpStatusCode.UnsupportedMediaType)
                        sb.Append($"Error al editar la {item.NombreMesa}\n");
                }
            }

            servicioDialogo.MostrarMensajeInformacion(sb.ToString(), "");
            CargarMesas();

            
        }

        public bool ComprobarCambiosPOST()
        {
            ObservableCollection<Mesa> listaAux = servicioAPIRestRestaurante.GetMesas();
            if (listaAux.Count != ListaMesas.Count)
            {
                foreach (Mesa item in ListaMesas)
                {
                    if (item.IdMesa == 0)
                        ListaPOST.Add(item);
                }
                return true;

            }
            return false;
        }

        public void ComprobarCambiosPUT()
        {
            ObservableCollection<Mesa> listaAux = servicioAPIRestRestaurante.GetMesas();
            for (int i = 0; i < listaAux.Count; i++)
            {
                if (listaAux[i].Capacidad != ListaMesas[i].Capacidad)
                    ListaPUT.Add(ListaMesas[i]);
                else if (listaAux[i].NombreMesa != ListaMesas[i].NombreMesa)
                    ListaPUT.Add(ListaMesas[i]);
            }
        }

        /*public bool ComprobarCambios()
        {
            bool result = false;
            ObservableCollection<Mesa> listaAux = servicioAPIRestRestaurante.GetMesas();
            if (listaAux.Count != ListaMesas.Count)
            {
                foreach (Mesa item in ListaMesas)
                {
                    if (item.IdMesa == 0)
                        ListaPOST.Add(item);
                }
                result = true;
            }
             

            for (int i = 0; i < listaAux.Count; i++)
            {
                if (listaAux[i].Capacidad != ListaMesas[i].Capacidad)
                    ListaPUT.Add(listaAux[i]);
                else if (listaAux[i].NombreMesa != ListaMesas[i].NombreMesa)
                    ListaPUT.Add(listaAux[i]);
            }

            return false;
        }*/

        public void AñadirNuevaMesa()
        {
            ListaMesas.Add(new Mesa());
        }



        public void EliminarMesa()
        {
            MessageBoxResult result = MessageBox.Show("¿Estas seguro de eliminar esta mesa?", "ADVERTENCIA", MessageBoxButton.YesNo, MessageBoxImage.Warning);
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

            return true;
        }
    }
}
