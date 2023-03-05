using DomainObjects;
using DomainObjects.Parameters;

namespace SmartInfo.ServiceInterfaces.IRepositories;

public interface IParametersRepository : IRepository<Parameter>
{
    ScreenWidth ScreenWidth { get; }
    ScreenHeight ScreenHeight { get; }
    BackgroundColor BackgroundColor { get; }
}