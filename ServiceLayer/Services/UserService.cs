using Microsoft.EntityFrameworkCore;
using ServiceLayer.Data;
using ServiceLayer.Models;

namespace ServiceLayer.Services
{
    public class UserService
    {
        public static readonly InformaticTextBookContext _context = new();

        public async Task<User?> GetUserByLoginAndPasswordAsync(string login, string password)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserLogin == login && u.UserPassword == password);
        }
    }
}
