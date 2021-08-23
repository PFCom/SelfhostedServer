using System.Linq;

namespace PFCom.Selfhosted.DataAccess
{
    public interface IRepository<T> where T : class
    {
        public IQueryable<T> GetAll();

        public T Get(params object[] primaryKey);
        
        public void Add(T entity);

        public void Update(T entity);

        public void Delete(T entity);

        public void DeleteAll();
    }
}
