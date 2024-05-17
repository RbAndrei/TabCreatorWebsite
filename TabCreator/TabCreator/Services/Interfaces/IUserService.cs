using TabCreator.Models;

namespace TabCreator.Services.Interfaces
{
    public interface IUserService
    {
        void CreateUser(User user);

        void DeleteUser(User user);

        void UpdateUser(User user);

        User GetUserById(int id);

        List<User> GetUserByName(string UserName);

        List<User> GetUsers();
    }
}

