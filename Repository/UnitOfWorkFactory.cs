using ServiceInterfaces;

namespace Repository
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        public IUnitOfWork Create()
        {
            return new EfUnitOfWork(new DatabaseContext());
        }
    }
}
