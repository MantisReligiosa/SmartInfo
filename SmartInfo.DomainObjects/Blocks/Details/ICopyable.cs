namespace DomainObjects.Blocks.Details;

public interface ICopyable<in T>
{
    void CopyFrom(T source);
}