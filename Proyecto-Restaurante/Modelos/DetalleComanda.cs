﻿using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.Modelos
{
    class DetalleComanda : ObservableObject
    {

        private int cantidad;
        [JsonProperty("cantidad")]
        public int Cantidad
        {
            get { return cantidad; }
            set { SetProperty(ref cantidad, value); }
        }

        private DetalleComandaPK _detallesComandaPK;
        [JsonProperty("detallescomandaPK")]
        public DetalleComandaPK _DetallesComandaPK
        {
            get { return _detallesComandaPK; }
            set { SetProperty(ref _detallesComandaPK, value); }
        }

        private Producto producto;
        [JsonProperty("productos")]
        public Producto Producto
        {
            get { return producto; }
            set { SetProperty(ref producto, value); }
        }

        private double importeTotalProducto;
        [JsonIgnore]
        public double ImporteTotalProducto
        {
            get => Cantidad * Producto.PrecioUnitario;
            set
            {
                SetProperty(ref importeTotalProducto, value);
            }
        }

        public void ActualizarImporteTotal()
        {
            if (Producto.PrecioUnitario != 0)
            {
                ImporteTotalProducto = Cantidad * Producto.PrecioUnitario;
            }
        }

        public DetalleComanda(int cantidad, Producto producto)
        {
            this.cantidad = cantidad;
            this.producto = producto;
        }

        public DetalleComanda(int cantidad, DetalleComandaPK detallesComandaPK, Producto producto)
        {
            this.cantidad = cantidad;
            _detallesComandaPK = detallesComandaPK;
            this.producto = producto;
        }

        public DetalleComanda()
        {
        }


    }
}
