using System.Linq;

namespace PFCom.Selfhosted.DataAccess
{
    public interface IBaseRepository<T> : IRepository<T> where T : class
    {
    }
}
