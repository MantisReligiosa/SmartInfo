using SmartInfo.ServiceInterfaces;

namespace SmartInfo.Services;

public class OperationController : IOperationController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBlockController _blockController;
    private readonly ISystemController _systemController;

    public OperationController(IBlockController blockController,
        ISystemController systemController,
        IUnitOfWorkFactory unitOfWorkFactory)
    {
        _unitOfWork = unitOfWorkFactory.Create();
        _blockController = blockController;
        _systemController = systemController;
    }

    public void StartShow()
    {
        throw new NotImplementedException();
    }

    public void StopShow()
    {
        throw new NotImplementedException();
    }

    public void ApplyChanges()
    {
        throw new NotImplementedException();
    }
}