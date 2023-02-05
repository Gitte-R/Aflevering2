using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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

        public Data.Models.Company company { get; set; }
        public IList<Data.Models.Report> companyReports { get; set; }
        public IList<Data.Models.ViewCompany> viewCompanyList { get; set; }
        public IList<Data.Models.Report> allReports { get; set; }
        public IList<Data.Models.Report>? allReportsById { get; set; }
        public ViewCompany viewcompany { get; set; }
        public IList<Data.Models.Company> companyList { get; set; }

        public IList<Data.Models.Report> GetAllReportsById(int? _id, IList<Data.Models.Report> _allReports)
        {
            companyReports = new List<Data.Models.Report>();

            foreach (var report in _allReports)
            {
                if (report.companyId == _id)
                {
                    companyReports.Add(report);
                }
            }

            companyReports = companyReports.OrderByDescending(x => x.date).ToList();
            return companyReports;
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            companyList = _context.Companies.ToList();
            viewCompanyList = new List<Data.Models.ViewCompany>();
            company = new Data.Models.Company();
            company = companyList.Where(x => x.id == id).First();

            viewcompany = new ViewCompany();
            viewcompany.id = company.id;
            viewcompany.companyName = company.companyName;
            viewcompany.companyAddress = company.companyAddress;
            viewcompany.cvr = company.cvr;

            allReports = _context.Reports.ToList();
            allReportsById = GetAllReportsById(id, allReports);
            viewcompany.allSmileyReports = allReportsById.ToList();

            var companY = await _context.Companies.FirstOrDefaultAsync(m => m.id == id);
            if (companY == null)
            {
                return NotFound();
            }
            else 
            {
                company = companY;
            }
            return Page();
        }
    }
}
