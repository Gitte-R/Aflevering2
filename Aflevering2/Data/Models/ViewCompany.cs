using Smiley.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Smiley.Data.Models
{
    public class ViewCompany : Company
    {
        [Display(Name = "Latest Smiley")]
        public SmileyEnum latestSmileyFace { get; set; }
    }
}
