using System.ComponentModel.DataAnnotations;

namespace Smiley.Data.Models
{
    public class Report
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        [Display(Name = "Company Id")]
        public int companyId { get; set; }
        [Display(Name = "Smiley")]
        public SmileyEnum smileyFace { get; set; }
    }
}
