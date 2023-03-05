namespace SmartInfo.ServiceInterfaces;

public interface IUnitOfWorkFactory
{
    IUnitOfWork Create();
}