namespace DomainObjects.Blocks.Details
{
    public interface ICopyable<T>
    {
        void CopyFrom(T source);
    }
}
