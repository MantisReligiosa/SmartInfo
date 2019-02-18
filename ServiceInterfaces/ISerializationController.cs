using System.IO;

namespace ServiceInterfaces
{
    public interface ISerializationController
    {
        Stream SerializeXML<T>(T source);
        T Deserialize<T>(Stream stream);
    }
}
