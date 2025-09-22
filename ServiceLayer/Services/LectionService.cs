using Microsoft.EntityFrameworkCore;
using ServiceLayer.Data;
using ServiceLayer.Models;

namespace ServiceLayer.Services
{
    public class LectionService
    {

        public static readonly InformaticTextBookContext _context = new();

        public async Task<List<Lection>?> GetLectionsByThemeIdAsync(int themeId)
        {
            return await _context.Lections.Include(l=>l.Theme).Where(l => l.ThemeId == themeId).ToListAsync();
        }
    }
}
