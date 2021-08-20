using System.Threading.Tasks;

namespace PFCom.Selfhosted.DataAccess
{
    public interface IUnitOfWork
    {
        public ITrasaction BeginTransaction();

        public Task<ITrasaction> BeginTransactionAsync();
        
        public void Complete();

        public Task CompleteAsync();
    }
}
