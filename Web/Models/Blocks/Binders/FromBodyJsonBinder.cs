using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Web.Models.Blocks.Binders
{
    public class FromBodyJsonBinder : IModelBinder
    {
        public object Bind(NancyContext context, Type modelType, object instance, BindingConfig configuration, params string[] blackList)
        {
            using (var sr = new StreamReader(context.Request.Body))
            {
                if (sr.EndOfStream)
                {
                    sr.BaseStream.Seek(0, SeekOrigin.Begin);
                }
                var json = sr.ReadToEnd();
                var obj =  JsonConvert.DeserializeObject(json, modelType);
                return obj;
            }
        }

        public object Bind(NancyContext context, Type modelType)
        {
            return Bind(context, modelType, null, new BindingConfig());
        }

        public bool CanBind(Type modelType)
        {
            return !modelType.Equals(typeof(CreditsRequest));
        }
    }
}