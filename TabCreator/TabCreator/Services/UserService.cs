using TabCreator.Models;
using TabCreator.Repositories.Interfaces;
using TabCreator.Services.Interfaces;

namespace TCG_Collector.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public UserService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public void CreateUser(User user)
        {
            _repositoryWrapper.UserRepository.Create(user);
            _repositoryWrapper.Save();
        }

        public void DeleteUser(User user)
        {
            _repositoryWrapper.UserRepository.Delete(user);
            _repositoryWrapper.Save();
        }

        public void UpdateUser(User user)
        {
            _repositoryWrapper.UserRepository.Update(user);
            _repositoryWrapper.Save();
        }

        public User GetUserById(int id)
        {
            return _repositoryWrapper.UserRepository.FindByCondition(c => c.UserId == id).FirstOrDefault()!;
        }

        public List<User> GetUserByName(string Name)
        {
            return _repositoryWrapper.UserRepository.FindByCondition(c => c.UserName == Name).ToList();
        }

        public List<User> GetUsers()
        {
            return _repositoryWrapper.UserRepository.FindAll().ToList();
        }

    }
}
