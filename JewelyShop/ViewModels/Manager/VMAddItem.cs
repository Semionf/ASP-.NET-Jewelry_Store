using JewelyShop.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace JewelyShop.ViewModels.Manager
{
    public class VMAddItem
    {
        public Item Item { get; set; }
        public Price Price { get; set; }

        [Display(Name = "הוספת תמונה לפריט")]
        public IFormFile File { get; set; }
        public Group Group { get; set; }
        public int GroupID { get; set; }
        public List<Group> Groups { get; set; }
    }
}
