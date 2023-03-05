namespace SmartInfo.ServiceInterfaces;

public interface ISerializationController
{
    Stream SerializeXML<T>(T source);
    T DeserializeXML<T>(string source) where T : class;
}