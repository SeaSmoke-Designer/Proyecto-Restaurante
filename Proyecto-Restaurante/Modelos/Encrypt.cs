using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.Modelos
{
    public class Encrypt
    {
        /*public string GetSHA256(string str)
        {
            SHA256 sHA256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sHA256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++)
                sb.AppendFormat("{0:x2}", stream[i]);
            
            return sb.ToString();
        }

        public string Desencriptar(string str)
        {
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();
            byte[] decryptData = RSAalg.Decrypt(Encoding.UTF8.GetBytes(str), false);
            return Encoding.UTF8.GetString(decryptData);
        }*/

        public string Encriptar(string _cadenaAencriptar)
        {
            if (_cadenaAencriptar is null || _cadenaAencriptar == "")
            {
                return "";
            }
            string result = string.Empty;
            byte[] encryted =
            System.Text.Encoding.Unicode.GetBytes(_cadenaAencriptar);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        /// Esta función "desencripta" la cadena que le envíamos en el parámentro de entrada.
        public string DesEncriptar(string _cadenaAdesencriptar)
        {
            if(_cadenaAdesencriptar is null || _cadenaAdesencriptar == "")
            {
                return "";
            }
            string result = string.Empty;
            byte[] decryted =
            Convert.FromBase64String(_cadenaAdesencriptar);
            //result = 
            System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }
    }
}
