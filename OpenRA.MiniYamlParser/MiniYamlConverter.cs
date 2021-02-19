using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace OpenRA.MiniYamlParser
{
    public abstract class MiniYamlConverter
    {
        public abstract bool CanConvert(Type typeToConvert);

        // This is used internally to quickly determine the type being converted for JsonConverter<T>.
        internal virtual Type TypeToConvert => null;
    }

    public abstract class MiniYamlConverter<T> : MiniYamlConverter
    {
        public abstract T Read(string value);
        public abstract string Write(T value);
    }


    public class Converters
    {
        readonly ConcurrentDictionary<Type, MiniYamlConverter> converters = new ConcurrentDictionary<Type, MiniYamlConverter>();

        public MiniYamlConverter GetConverter(Type type)
        {
            if (converters.TryGetValue(type, out var converter))
                return converter;

            return null;
        }
    }

}