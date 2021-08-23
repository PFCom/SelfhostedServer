using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PFCom.Selfhosted.DataAccess.EFCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context { get; }

        public UnitOfWork(DataContext context)
        {
            this._context = context;
        }
        
        public ITransaction BeginTransaction()
        {
            var trans = this._context.Database.BeginTransaction();

            return new Transaction(trans);
        }

        public async Task<ITransaction> BeginTransactionAsync()
        {
            var trans = await this._context.Database.BeginTransactionAsync();

            return new Transaction(trans);
        }

        public void Complete()
        {
            this._context.SaveChanges();
        }

        public Task CompleteAsync()
        {
            return this._context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            this._context.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return this._context.SaveChangesAsync();
        }
    }
}
