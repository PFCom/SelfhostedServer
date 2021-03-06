using System.Linq;

namespace PFCom.Selfhosted.DataAccess
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private IBaseRepository<TEntity> _repository { get; }

        protected Repository(IBaseRepository<TEntity> repository)
        {
            this._repository = repository;
        }

        public IQueryable<TEntity> GetAll() => this._repository.GetAll();

        public TEntity Get(params object[] primaryKey) => this._repository.Get(primaryKey);

        public void Add(TEntity entity) => this._repository.Add(entity);

        public void Update(TEntity entity) => this._repository.Update(entity);

        public void Delete(TEntity entity) => this._repository.Delete(entity);
        public void DeleteAll() => this._repository.DeleteAll();
    }
}
