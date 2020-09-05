using DataExchange;
using DataExchange.Requests;
using ServiceInterfaces;

namespace Services
{
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
            var broker = Broker.GetBroker();
            var request = new StartShowRequest
            {
                Background = _blockController.GetBackground(),
                Blocks = _blockController.GetBlocks(),
                Screens = _systemController.GetDatabaseScreenInfo()
            };
            broker.GetResponce(request);
        }

        public void StopShow()
        {
            var broker = Broker.GetBroker();
            broker.GetResponce(new StopShowRequest());
        }

        public void ApplyChanges()
        {
            var broker = Broker.GetBroker();
            var request = new ApplyChangesRequest
            {
                Background = _blockController.GetBackground(),
                Blocks = _blockController.GetBlocks()
            };
            broker.GetResponce(request);
        }
    }
}
