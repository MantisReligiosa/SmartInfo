using ServiceInterfaces;

namespace Repository
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IConfiguration _configuration;

        public UnitOfWorkFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IUnitOfWork Create()
        {
            return new EfUnitOfWork(new DatabaseContext());
        }
    }
}
