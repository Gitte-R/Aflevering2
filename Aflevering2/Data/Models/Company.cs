using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Smiley.Data.Models
{
    public class Company
    {
        public int id { get; set; }

        [Display(Name = "Name of Company")]
        public string companyName { get; set; }
        public int cvr { get; set; }
    
        [Display(Name = "Address of Company")]
        public string? companyAddress { get; set; }
        public ICollection<Report>? companyReports { get; set; }
    }
}
