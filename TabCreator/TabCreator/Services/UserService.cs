using Microsoft.AspNetCore.Identity;
using TabCreator.Services.Interfaces;
using TabCreator.Repositories.Interfaces;

namespace TabCreator.Services
{
    public class UserService: IUserService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public UserService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public void CreateUser(IdentityUser user)
        {
            _repositoryWrapper.UserRepository.Create(user);
            _repositoryWrapper.Save();
        }

        public void DeleteUser(IdentityUser user)
        {
            _repositoryWrapper.UserRepository.Delete(user);
            _repositoryWrapper.Save();
        }

        public void UpdateUser(IdentityUser user)
        {
            _repositoryWrapper.UserRepository.Update(user);
            _repositoryWrapper.Save();
        }

        public IdentityUser GetUserById(string id)
        {
            return _repositoryWrapper.UserRepository.FindByCondition(c => c.Id == id).FirstOrDefault()!;
        }

        public List<IdentityUser> GetUsers()
        {
            return _repositoryWrapper.UserRepository.FindAll().ToList();
        }
    }
}
