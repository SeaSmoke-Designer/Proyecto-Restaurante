﻿using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.Servicios
{
    class ServicioAzure
    {
        private readonly string claveConexion = Properties.Settings.Default.ClaveAzure;
        private readonly string nombreContenedorProductosAzure = Properties.Settings.Default.NombreContenedorProductos;
        private readonly string nombreContenedorEmpleadosAzure = Properties.Settings.Default.NombreContenedorEmpleados;

        public string AlmacenarImagenProductoNube(string rutaImagen)
        {
            if (rutaImagen != "")
            {
                var clienteBlobService = new BlobServiceClient(claveConexion);
                var clienteContenedor = clienteBlobService.GetBlobContainerClient(nombreContenedorProductosAzure);

                Stream streamImagen = File.OpenRead(rutaImagen);
                string nombreImagen = Path.GetFileName(rutaImagen);

                if (!clienteContenedor.GetBlobClient(nombreImagen).Exists())
                    clienteContenedor.UploadBlob(nombreImagen, streamImagen);

                var clienteBlobImagen = clienteContenedor.GetBlobClient(nombreImagen);

                return clienteBlobImagen.Uri.AbsoluteUri;
            }
            else return "";

        }

        public string AlmacenarImagenEmpleadoNube(string rutaImagen)
        {
            if (rutaImagen != "")
            {
                var clienteBlobService = new BlobServiceClient(claveConexion);
                var clienteContenedor = clienteBlobService.GetBlobContainerClient(nombreContenedorEmpleadosAzure);

                Stream streamImagen = File.OpenRead(rutaImagen);
                string nombreImagen = Path.GetFileName(rutaImagen);

                if (!clienteContenedor.GetBlobClient(nombreImagen).Exists())
                    clienteContenedor.UploadBlob(nombreImagen, streamImagen);

                var clienteBlobImagen = clienteContenedor.GetBlobClient(nombreImagen);

                return clienteBlobImagen.Uri.AbsoluteUri;
            }
            else return "";

        }
        /*public string AlmacenarImagenCategoriaNube(string rutaImagen)
        {
            if (rutaImagen != "")
            {
                var clienteBlobService = new BlobServiceClient(claveConexion);
                var clienteContenedor = clienteBlobService.GetBlobContainerClient(nombreContenedorProductosAzure);

                Stream streamImagen = File.OpenRead(rutaImagen);
                string nombreImagen = Path.GetFileName(rutaImagen);

                if (!clienteContenedor.GetBlobClient(nombreImagen).Exists())
                    clienteContenedor.UploadBlob(nombreImagen, streamImagen);

                var clienteBlobImagen = clienteContenedor.GetBlobClient(nombreImagen);

                return clienteBlobImagen.Uri.AbsoluteUri;
            }
            else return "";

        }*/

    }
}
