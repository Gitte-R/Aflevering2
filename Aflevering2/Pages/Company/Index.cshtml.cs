using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Smiley.Data;
using Smiley.Data.Models;

namespace Smiley.Pages.Company
{
    public class IndexModel : PageModel
    {
        private readonly Smiley.Data.ApplicationDbContext _context;

        public IndexModel(Smiley.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Data.Models.Company> Company { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Companies != null)
            {
                Company = await _context.Companies.ToListAsync();
            }
        }
    }
}
