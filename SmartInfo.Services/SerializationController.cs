using System.Xml.Serialization;
using SmartInfo.ServiceInterfaces;

namespace SmartInfo.Services;

public class SerializationController : ISerializationController
{
    public T DeserializeXML<T>(string source) where T : class
    {
        var serializer = new XmlSerializer(typeof(T));
        using TextReader reader = new StringReader(source);
        var obj = serializer.Deserialize(reader);
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