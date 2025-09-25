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

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<List<User>> GetAllStudentsAsync()
        {
            return await _context.Users.Where(u => u.RoleId == 2).Include(u => u.Role).ToListAsync();
        }
    }
}
