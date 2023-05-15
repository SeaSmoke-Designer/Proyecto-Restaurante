using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using Proyecto_Restaurante.Convertidores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.Modelos
{
    class Comanda: ObservableObject
    {
        private int idComanda;
        [JsonProperty("idComanda")]
        public int IdComanda
        {
            get { return idComanda; }
            set { SetProperty(ref idComanda, value); }
        }

        private Empleado empleado;
        [JsonProperty("idEmpleado")]
        public Empleado Empleado
        {
            get { return empleado; }
            set { SetProperty(ref empleado, value); }
        }

        private Mesa mesa;
        [JsonProperty("idMesa")]
        public Mesa Mesa
        {
            get { return mesa; }
            set { SetProperty(ref mesa, value); }
        }

        private int cantidadPersonas;
        [JsonProperty("cantidadPersonas")]
        public int CantidadPersonas
        {
            get { return cantidadPersonas; }
            set { SetProperty(ref cantidadPersonas, value); }
        }

        private bool pagada;
        [JsonProperty("pagada")]
        public bool Pagada
        {
            get { return pagada; }
            set { SetProperty(ref pagada, value); }
        }

        private DateTime fecha;
        [JsonProperty("fecha")]
        [JsonConverter(typeof(UTCDateTimeConverter))]
        public DateTime Fecha
        {
            get { return fecha; }
            set { SetProperty(ref fecha, value); }
        }

        

        private ObservableCollection<DetalleComanda> detallescomandaCollection;
        [JsonProperty("detallescomandaCollection")]
        public ObservableCollection<DetalleComanda> DetallescomandaCollection
        {
            get { return detallescomandaCollection; }
            set { SetProperty(ref detallescomandaCollection, value); }
        }

        public Comanda()
        {
        }

        public Comanda(int idComanda, Empleado empleado, Mesa mesa, int cantidadPersonas, bool pagada, DateTime fecha, ObservableCollection<DetalleComanda> detallescomandaCollection)
        {
            this.idComanda = idComanda;
            this.empleado = empleado;
            this.mesa = mesa;
            this.cantidadPersonas = cantidadPersonas;
            this.pagada = pagada;
            this.fecha = fecha;
            this.detallescomandaCollection = detallescomandaCollection;
        }
    }
}
