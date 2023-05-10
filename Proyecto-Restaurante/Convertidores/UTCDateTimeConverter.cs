using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Restaurante.Convertidores
{
    class UTCDateTimeConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null) return null;
            return DateTime.TryParseExact(reader.Value.ToString(), "yyyy-MM-ddTHH:mm:ssZ[UTC]", null, DateTimeStyles.None, out DateTime result)
                ? result.ToUniversalTime()
                : (object)null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((DateTime)value).ToString("yyyy-MM-ddTHH:mm:ssZ[UTC]"));
        }
    }

    
}
