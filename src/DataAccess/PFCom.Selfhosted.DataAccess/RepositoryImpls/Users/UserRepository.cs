using PFCom.Selfhosted.Core.Users;
using PFCom.Selfhosted.DataAccess.Repositories.Users;

namespace PFCom.Selfhosted.DataAccess.RepositoryImpls.Users
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IBaseRepository<User> repository) : base(repository)
        {
        }
    }
}
