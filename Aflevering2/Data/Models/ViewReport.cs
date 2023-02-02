using Smiley.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Smiley.Data.Models
{
    public class ViewReport : Report
    {
        [Display(Name = "Name of Company")]
        public string companyName { get; set; }
    }
}
