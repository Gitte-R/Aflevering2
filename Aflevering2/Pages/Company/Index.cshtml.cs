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
        public IList<Data.Models.Report> allReports { get; set; }
        public IList<Data.Models.Report>? latestReportList { get; set; }


        public async Task OnGetAsync()
        {
            if (_context.Companies != null)
            {
                companyList = await _context.Companies.ToListAsync();
            }

            #region Generate viewCompanyList
            viewCompanyList = new List<Data.Models.ViewCompany>();

            foreach (var company in companyList)
            {
                ViewCompany viewcompany = new ViewCompany();

                viewcompany.id = company.id;
                viewcompany.companyName = company.companyName;
                viewcompany.companyAddress = company.companyAddress;
                viewcompany.cvr = company.cvr;


                allReports = _context.Reports.ToList();

                latestReportList = allReports.GroupBy(obj => obj.companyId).Select(grp => grp.OrderByDescending(obj => obj.date).First()).ToList();

                //for (int i = 0; i < companyList.Count; i++)
                //{
                //    //latestReportList = new List<Data.Models.Report>();
                //    latestReportList = companyList[i].GroupBy(obj => obj.companyId).Select(grp => grp.OrderByDescending(obj => obj.date).First()).ToList();
                //}

                //latestReportList skal for hver id oprette en liste af smiley. Og denne liste gemmes i en ny liste. Så kan Man altid finde seneste som index 0 og tre foregående som idex 1,2 og 3. Hvis der er så mange.
                //List<User> list = new List<User>();

                //foreach (string s in l)
                //{
                //    User u = new User();
                //    u.Name = s;
                //    list.Add(u);
                //}

                for (int i = 0; i < latestReportList.Count(); i++)
                {
                    if (latestReportList[i].companyId == viewcompany.id)
                    {
                        viewcompany.latestSmileyFace = latestReportList[i].smileyFace;
                    }
                }
                viewCompanyList.Add(viewcompany);
            }
            #endregion
        }
    }
}
