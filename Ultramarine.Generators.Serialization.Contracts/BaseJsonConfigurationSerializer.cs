﻿using Newtonsoft.Json;
using System;
using System.IO;

namespace Ultramarine.Generators.Serialization.Contracts
{
    public abstract class BaseJsonConfigurationSerializer<T> : JsonSerializer, IConfigurationSerializer<T>
    {
        public string Path { get; private set; }
        public JsonConverter[] ConverterCollection { get; private set; }

        public BaseJsonConfigurationSerializer(string path, JsonConverter[] converters)
        {
            Path = path;
            ConverterCollection = converters;
        }
        public T Load()
        {
            var file = File.ReadAllText(Path);
            var generator = JsonConvert.DeserializeObject<T>(file, ConverterCollection);
            return generator;
        }
    }
}
