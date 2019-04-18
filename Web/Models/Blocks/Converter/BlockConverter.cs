using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Web.Models.Blocks.Converter
{
    public class BlockConverter : JsonConverter
    {
        private static readonly JsonSerializerSettings _specifiedSubclassConversion = new JsonSerializerSettings()
        {
            ContractResolver = new BaseSpecifiedConcreteClassConverter()
        };

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(BlockDto));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var type = jObject["type"].Value<string>();
            switch (type)
            {
                case "meta":
                    return JsonConvert.DeserializeObject<MetaBlockDto>(jObject.ToString(), _specifiedSubclassConversion);
                case "datetime":
                    return JsonConvert.DeserializeObject<DateTimeBlockDto>(jObject.ToString(), _specifiedSubclassConversion);
                case "picture":
                    return JsonConvert.DeserializeObject<PictureBlockDto>(jObject.ToString(), _specifiedSubclassConversion);
                case "table":
                    return JsonConvert.DeserializeObject<TableBlockDto>(jObject.ToString(), _specifiedSubclassConversion);
                case "text":
                    return JsonConvert.DeserializeObject<TextBlockDto>(jObject.ToString(), _specifiedSubclassConversion);
            }
            throw new NotImplementedException();
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException(); // won't be called because CanWrite returns false
        }
    }
}