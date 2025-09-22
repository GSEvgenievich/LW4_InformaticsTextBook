using Microsoft.EntityFrameworkCore;
using ServiceLayer.Data;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ThemeService
    {
        public static readonly InformaticTextBookContext _context = new();

        public async Task<List<Theme>?> GetThemesAsync()
        {
            return await _context.Themes.ToListAsync();
        }
    }
}
