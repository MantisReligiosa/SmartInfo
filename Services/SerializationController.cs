using System.IO;
using System.Xml.Serialization;
using ServiceInterfaces;

namespace Services
{
    public class SerializationController : ISerializationController
    {
        public T Deserialize<T>(Stream stream)
        {
            throw new System.NotImplementedException();
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
