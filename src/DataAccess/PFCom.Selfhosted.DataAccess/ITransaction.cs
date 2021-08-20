using System.Threading.Tasks;

namespace PFCom.Selfhosted.DataAccess
{
    public interface ITrasaction
    {
        public void Commit();

        public Task CommitAsync();

        public void Rollback();

        public Task RollbackAsync();
    }
}
