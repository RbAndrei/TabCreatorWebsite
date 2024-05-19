using Microsoft.AspNetCore.Identity;

namespace TabCreator.Repositories.Interfaces
{
    public interface IUserRepository : IRepositoryBase<IdentityUser>
    {
    }
}
