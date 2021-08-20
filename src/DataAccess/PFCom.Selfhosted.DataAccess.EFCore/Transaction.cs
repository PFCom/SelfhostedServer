using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace PFCom.Selfhosted.DataAccess.EFCore
{
    public class Trasaction : ITrasaction
    {
        private IDbContextTransaction _transaction { get; }

        public Trasaction(IDbContextTransaction transaction)
        {
            this._transaction = transaction;
        }

        public void Commit() => this._transaction.Commit();

        public Task CommitAsync() => this._transaction.CommitAsync();

        public void Rollback() => this._transaction.Rollback();

        public Task RollbackAsync() => this._transaction.RollbackAsync();
    }
}
