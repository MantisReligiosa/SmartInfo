using ServiceInterfaces;

namespace Web.Modules
{
    public class OperationModule : WrappedNancyModule
    {
        private readonly IOperationController _operationController;

        public OperationModule(IOperationController operationController)
            : base()
        {
            Post["/api/startShow"] = Wrap(StartShow, "Ошибка запуска полноэкранного режима");
            Post["/api/stopShow"] = Wrap(StopShow, "Ошибка остановки полноэкранного режима");
        }

        private void StopShow()
        {
            _operationController.StopShow();
        }

        private void StartShow()
        {
            _operationController.StartShow();
        }
    }
}