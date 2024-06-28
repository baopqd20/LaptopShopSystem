using LaptopShopSystem.Dto;
using LaptopShopSystem.Models;

namespace LaptopShopSystem.Interfaces
{
    public interface IUserRepository
    {
        bool CreateUser(User user);
        bool Save();      
        bool CheckEmail(string email);
        bool CheckPassword(UserLoginDto userLogin, string pass);
        User GetUserByEmail(string email);
        string GetRole(User user);
        ICollection<User> GetUsers();
        User GetUserById(int id);
        bool UpdateUser(User user);
        bool UserExists(int userId);
        bool DeleteUser(User user);

    }
}
