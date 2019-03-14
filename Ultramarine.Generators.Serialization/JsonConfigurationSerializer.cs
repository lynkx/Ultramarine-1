﻿using Newtonsoft.Json;
using Ultramarine.Generators.LanguageDefinitions;
using Ultramarine.Generators.Serialization.Contracts;

namespace Ultramarine.Generators.Serialization
{
    public class JsonConfigurationSerializer<T> : BaseJsonConfigurationSerializer<T> where T : IGenerator
    {
        public JsonConfigurationSerializer(string path, JsonConverter[] converters) : base(path, converters)
        {

        }
    }
}