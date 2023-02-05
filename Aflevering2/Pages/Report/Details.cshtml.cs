using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Smiley.Data;
using Smiley.Data.Models;

namespace Smiley.Pages.Report
{
    public class DetailsModel : PageModel
    {
        private readonly Smiley.Data.ApplicationDbContext _context;

        public DetailsModel(Smiley.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Data.Models.Report Report { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Reports == null)
            {
                return NotFound();
            }

            var report = await _context.Reports.FirstOrDefaultAsync(m => m.id == id);
            if (report == null)
            {
                return NotFound();
            }
            else 
            {
                Report = report;
            }
            return Page();
        }
    }
}
