using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Smiley.Data;
using Smiley.Data.Models;

namespace Smiley.Pages.Company
{
    public class EditModel : PageModel
    {
        private readonly Smiley.Data.ApplicationDbContext _context;

        public EditModel(Smiley.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Data.Models.Company Company { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company =  await _context.Companies.FirstOrDefaultAsync(m => m.id == id);
            if (company == null)
            {
                return NotFound();
            }
            Company = company;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Company).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(Company.id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CompanyExists(int id)
        {
          return _context.Companies.Any(e => e.id == id);
        }
    }
}
