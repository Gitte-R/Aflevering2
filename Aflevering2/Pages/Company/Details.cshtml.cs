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
    public class DetailsModel : PageModel
    {
        private readonly Smiley.Data.ApplicationDbContext _context;

        public DetailsModel(Smiley.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Data.Models.Company Company { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.FirstOrDefaultAsync(m => m.id == id);
            if (company == null)
            {
                return NotFound();
            }
            else 
            {
                Company = company;
            }
            return Page();
        }
    }
}
