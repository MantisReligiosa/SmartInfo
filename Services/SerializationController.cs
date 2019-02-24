using ServiceInterfaces;
using System.IO;
using System.Xml.Serialization;

namespace Services
{
    public class SerializationController : ISerializationController
    {
        public T Deserialize<T>(string source) where T : class
        {
            var serializer = new XmlSerializer(typeof(T));
            object obj;
            using (TextReader reader = new StringReader(source))
            {
                obj = serializer.Deserialize(reader);
            }
            var result =  obj as T;
            return result;
        }

        public Stream SerializeXML<T>(T source)
        {
            var serializer = new XmlSerializer(typeof(T));
            var stream = new MemoryStream();
            serializer.Serialize(stream, source);
            stream.Seek(0, SeekOrigin.Begin);
            //stream.Close();
            return stream;
        }
    }
}
