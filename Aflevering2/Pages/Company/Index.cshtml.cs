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
        public IList<Data.Models.Company> companyList { get;set; }
        public IList<Data.Models.ViewCompany> viewCompanyList { get; set; }
        public ViewCompany viewcompany { get; set; }
        public IList<Data.Models.Report> allReports { get; set; }
        public IList<Data.Models.Report> companyReports { get; set; }
        public IList<Data.Models.Report>? allReportsById { get; set; }

        public IList<Data.Models.Report> GetAllReportsById(int _id, IList<Data.Models.Report> _allReports)
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


        public async Task OnGetAsync()
        {
            if (_context.Companies != null)
            {
                companyList = await _context.Companies.ToListAsync();
            }

            viewCompanyList = new List<Data.Models.ViewCompany>();

            foreach (var company in companyList)
            {
                viewcompany = new ViewCompany();

                viewcompany.id = company.id;
                viewcompany.companyName = company.companyName;
                viewcompany.companyAddress = company.companyAddress;
                viewcompany.cvr = company.cvr;
                viewCompanyList.Add(viewcompany);

                allReports = _context.Reports.ToList();

                allReportsById = GetAllReportsById(company.id, allReports);
                viewcompany.allSmileyReports = allReportsById.ToList();
                viewcompany.numberOfReports = allReportsById.Count;
            }
        }
    }
}
