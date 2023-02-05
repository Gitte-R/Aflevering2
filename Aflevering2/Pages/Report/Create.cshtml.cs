using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Smiley.Data;
using Smiley.Data.Models;

namespace Smiley.Pages.Report
{
    public class CreateModel : PageModel
    {
        private readonly Smiley.Data.ApplicationDbContext _context;

        public CreateModel(Smiley.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Data.Models.Company> allCompanies { get; set; }
        public IList<Data.Models.Report> allReports { get; set; }
        public IList<SmileyEnum> allSmileys { get; set; }
        [BindProperty]
        public Data.Models.Report report { get; set; }

        public IActionResult OnGet()
        {
            allCompanies = _context.Companies.ToList();
            allSmileys = (IList<SmileyEnum>)Enum.GetValues(typeof(SmileyEnum));
            allReports = _context.Reports.Distinct().ToList();

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            allCompanies = _context.Companies.ToList();

            allSmileys = (IList<SmileyEnum>)Enum.GetValues(typeof(SmileyEnum));

            if (!ModelState.IsValid)
            {
                return Page();
            }
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
