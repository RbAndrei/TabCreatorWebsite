using Microsoft.AspNetCore.Identity;
using TabCreator.Models;
using TabCreator.Repositories.Interfaces;

namespace TabCreator.Repositories
{
    public class UserRepository : RepositoryBase<IdentityUser>, IUserRepository
    {
        public UserRepository(TabCreatorContext tabCreatorContext)
            : base(tabCreatorContext) 
        { 
        }
    }
}
