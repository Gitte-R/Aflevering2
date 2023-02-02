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
        public string currentFilter { get; set; }
        public IList<Data.Models.Report> reportList { get; set; }
        public IList<Data.Models.Company> allCompanies { get; set; }
        public IList<Data.Models.ViewReport> viewReportsList { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchID)
        {
            dateSort = sortOrder == "Date" ? "Date_desc" : "Date";
            smileySort = sortOrder == "SmileyFace" ? "SmileyFace_desc" : "SmileyFace";
            IQueryable<Data.Models.Report> companyReports = _context.Reports;
            currentFilter = searchID;

            #region Filter table
            if (!String.IsNullOrEmpty(searchID))
            {
                companyReports = companyReports.Where(s => s.companyId.ToString().Equals(searchID));
            }
            #endregion

            #region Column sorting
            companyReports = sortOrder switch
            {
                "Date" => companyReports.OrderBy(s => s.date),
                "Date_desc" => companyReports.OrderByDescending(s => s.date),
                "SmileyFace" => companyReports.OrderBy(s => s.smileyFace),
                "SmileyFace_desc" => companyReports.OrderByDescending(s => s.smileyFace),
                _ => companyReports.OrderBy(s => s.date),
            };
            #endregion

            reportList = await companyReports.AsNoTracking().ToListAsync();

            #region Generate viewReportList
            viewReportsList = new List<Data.Models.ViewReport>();
            
            foreach(var report in reportList)
            {
                ViewReport viewreport = new ViewReport();

                viewreport.id = report.id;
                viewreport.date = report.date;
                viewreport.smileyFace = report.smileyFace;
                viewreport.companyId = report.companyId;

                allCompanies = _context.Companies.ToList();

                for (int i = 0; i < allCompanies.Count(); i++)
                {
                    if (allCompanies[i].id == viewreport.companyId)
                    {
                        viewreport.companyName = allCompanies[i].companyName;
                    }
                }
                viewReportsList.Add(viewreport);
            }
            #endregion
        }
    }
}

