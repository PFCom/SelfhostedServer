using System.Linq;
using PFCom.Selfhosted.Core.Users;
using PFCom.Selfhosted.DataAccess.Repositories.Users;

namespace PFCom.Selfhosted.DataAccess.RepositoryImpls.Users
{
    public class LocalUserRepository : Repository<LocalUser>, ILocalUserRepository
    {
        public LocalUserRepository(IBaseRepository<LocalUser> repository) : base(repository)
        {
        }
    }
}
