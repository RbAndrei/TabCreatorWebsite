using TabCreator.Models;
using TabCreator.Repositories.Interfaces;

namespace TabCreator.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(TabCreatorContext tabCreatorContext)
            : base(tabCreatorContext)
        {
        }
    }
}
