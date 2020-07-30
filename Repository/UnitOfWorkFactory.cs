using ServiceInterfaces;

namespace Repository
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IConfiguration _configuration;

        private static IUnitOfWork _unitOfWork;

        public UnitOfWorkFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IUnitOfWork Create()
        {
            if (_unitOfWork == null)
            {
                _unitOfWork = new EfUnitOfWork(new DatabaseContext());
            }
            return _unitOfWork;
        }
    }
}
