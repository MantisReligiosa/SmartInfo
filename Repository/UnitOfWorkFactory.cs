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
            var connStr = _configuration.ConnectionString;
            return new EfUnitOfWork(new DatabaseContext(connStr));
        }
    }
}
