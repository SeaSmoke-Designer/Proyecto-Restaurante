﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.Modelos
{
    class Comanda: ObservableObject
    {
        private int idComanda;

        public int IdComanda
        {
            get { return idComanda; }
            set { SetProperty(ref idComanda, value); }
        }

        private Empleado empleado;

        public Empleado Empleado
        {
            get { return empleado; }
            set { SetProperty(ref empleado, value); }
        }

        private Mesa mesa;

        public Mesa Mesa
        {
            get { return mesa; }
            set { SetProperty(ref mesa, value); }
        }

        private int cantidadPersonas;

        public int CantidadPersonas
        {
            get { return cantidadPersonas; }
            set { SetProperty(ref cantidadPersonas, value); }
        }

        private bool pagada;

        public bool Pagada
        {
            get { return pagada; }
            set { SetProperty(ref pagada, value); }
        }

        private DateTime fecha;

        public DateTime Fecha
        {
            get { return fecha; }
            set { SetProperty(ref fecha, value); }
        }

        private ObservableCollection<DetalleComanda> detallescomandaCollection;

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
            IdComanda = idComanda;
            Empleado = empleado;
            Mesa = mesa;
            CantidadPersonas = cantidadPersonas;
            Pagada = pagada;
            Fecha = fecha;
            DetallescomandaCollection = detallescomandaCollection;
        }
    }
}
