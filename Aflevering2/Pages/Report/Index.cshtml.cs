using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Smiley.Data;
using Smiley.Data.Models;

namespace Smiley.Pages.Report
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public string dateSort { get; set; }
        public string smileySort { get; set; }
        public string idSort { get; set; }
        public string companyIdSort { get; set; }
        public string currentFilter { get; set; }

        public IList<Data.Models.Report> reportList { get; set; }
        public IList<Data.Models.Company> allCompanies { get; set; }
        public Data.Models.Company reportCompany { get; set; }
        public Data.Models.Report report { get; set; }

        public void getCompanyName(int _idInput)
        {
            allCompanies = _context.Companies.ToList();
            reportList = _context.Reports.ToList();

            foreach (var company in allCompanies)
            {
                if (_idInput == report.companyId)
                {
                    
                }
            }
        }

        public async Task OnGetAsync(string sortOrder, string searchID)
        {
            dateSort    = sortOrder     == "Date" ? "Date_desc" : "Date";
            smileySort  = sortOrder     == "SmileyFace" ? "SmileyFace_desc" : "SmileyFace";
            idSort      = sortOrder     == "Id" ? "Id_desc" : "Id";
            companyIdSort = sortOrder   == "CompanyId" ? "CompanyId_desc" : "CompanyId";

            currentFilter = searchID;

            IQueryable<Data.Models.Report> companyReports = _context.Reports;

            if (!String.IsNullOrEmpty(searchID))
            {
                companyReports = companyReports.Where(s => s.companyId.ToString().Equals(searchID));
            }

            companyReports = sortOrder switch
            {
                "Id" => companyReports.OrderBy(s => s.id),
                "Id_desc" => companyReports.OrderByDescending(s => s.id),
                "Date" => companyReports.OrderBy(s => s.date),
                "Date_desc" => companyReports.OrderByDescending(s => s.date),
                "SmileyFace" => companyReports.OrderBy(s => s.smileyFace),
                "SmileyFace_desc" => companyReports.OrderByDescending(s => s.smileyFace),
                "CompanyId" => companyReports.OrderBy(s => s.companyId),
                "CompanyId_desc" => companyReports.OrderByDescending(s => s.companyId),
                _ => companyReports.OrderBy(s => s.id),
            };
            //IQueryable<Data.Models.Company> allCompanies = _context.Companies;

            reportList = await companyReports.AsNoTracking().ToListAsync();
        }

    }
}

