using BecauseImClever.DeviceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AutomationWebApi
{
    public class DeviceDeserializer : JsonConverter<Device>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsSubclassOf(typeof(Device)) ;
        }

        public override Device Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            while (reader.Read())
            {
                Console.Write(reader.TokenType);

                switch (reader.TokenType)
                {
                    case JsonTokenType.PropertyName:
                    case JsonTokenType.String:
                        {
                            string text = reader.GetString();
                            Console.Write(" ");
                            Console.Write(text);
                            break;
                        }

                    case JsonTokenType.Number:
                        {
                            int value = reader.GetInt32();
                            Console.Write(" ");
                            Console.Write(value);
                            break;
                        }

                        // Other token types elided for brevity
                }
            }

            Console.WriteLine();
            return new GenericDevice();
        }

        public override void Write(Utf8JsonWriter writer, Device value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
