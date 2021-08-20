using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PFCom.Selfhosted.DataAccess.EFCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext _context { get; }

        public UnitOfWork(DbContext context)
        {
            this._context = context;
        }
        
        public ITrasaction BeginTransaction()
        {
            var trans = this._context.Database.BeginTransaction();

            return new Trasaction(trans);
        }

        public async Task<ITrasaction> BeginTransactionAsync()
        {
            var trans = await this._context.Database.BeginTransactionAsync();

            return new Trasaction(trans);
        }

        public void Complete()
        {
            this._context.SaveChanges();
        }

        public Task CompleteAsync()
        {
            return this._context.SaveChangesAsync();
        }
    }
}
