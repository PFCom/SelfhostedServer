using System.Threading.Tasks;

namespace PFCom.Selfhosted.DataAccess
{
    public interface IUnitOfWork
    {
        public ITransaction BeginTransaction();

        public Task<ITransaction> BeginTransactionAsync();
        
        public void Complete();

        public Task CompleteAsync();
        public void SaveChanges();

        public Task SaveChangesAsync();
    }
}
