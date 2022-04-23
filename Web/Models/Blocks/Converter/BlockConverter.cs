using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

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
            if (jObject["type"].Value<string>().Equals("meta", StringComparison.InvariantCultureIgnoreCase))
                return DeserializeScenario(jObject);
            return DeserializeSimpleBlock(jObject);
        }

        private static BlockDto DeserializeSimpleBlock(JObject jObject)
        {
            switch (jObject["type"].Value<string>())
            {
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

        private static object DeserializeScenario(JObject jScenarioObject)
        {
            var scenarioDto = JsonConvert.DeserializeObject<ScenarioDto>(jScenarioObject.ToString(), _specifiedSubclassConversion);
            foreach (var jFrame in jScenarioObject["scenes"].AsJEnumerable())
            {
                if (jFrame["id"] != null)
                {
                    var id = int.Parse(jFrame["id"].Value<string>());
                    var sceneDto = scenarioDto.Scenes.First(f => f.Id.Equals(id));
                    sceneDto.Blocks = jFrame["blocks"].AsJEnumerable().Select(jBlock => DeserializeSimpleBlock(jBlock as JObject)).ToList();
                }
            }
            return scenarioDto;
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