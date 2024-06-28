using LaptopShopSystem.Data;
using LaptopShopSystem.Dto;
using LaptopShopSystem.Interfaces;
using LaptopShopSystem.Models;

namespace LaptopShopSystem.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public bool CheckEmail(string email)
        {   
            var valEmail = email.ToLower();
            var CheckUser = _context.Users.Where(p => p.Email.ToLower() == valEmail).FirstOrDefault();
            if (CheckUser == null)
            {
                return false;
            }
            return true;
        }

        public bool CheckPassword(UserLoginDto userLogin, string pass)
        {
            var CheckUser = _context.Users.Where(p => p.Password == pass).FirstOrDefault();
            if (CheckUser == null)
            {
                return false;
            }
            return true;
        }

        public bool CreateUser(User user)
        {   
            _context.Add(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public string GetRole(User user)
        {
            return user.Role;
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.Where(p => p.Email == email).FirstOrDefault();
        }

        public User GetUserById(int id)
        {
            return _context.Users.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool Save()
        {
            int saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool UserExists(int userId)
        {
            var user = _context.Users.Any(p => p.Id == userId);
            if(user == null)
            {
                return false;
            }
            return true;
        }
    }
}
