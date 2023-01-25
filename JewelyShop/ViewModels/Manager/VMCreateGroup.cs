using JewelyShop.Models;
using System.ComponentModel.DataAnnotations;

namespace JewelyShop.ViewModels.Manager
{
    public class VMCreateGroup
    {
        public VMCreateGroup() { Group = new Group(); Groups = new List<Group>(); Item = new Item(); Price = new Price(); }
        // הצגת כל הקבוצות הקיימות שאפשר להוסיף להם תת קבוצה
        public List<Group> Groups { get; set; }
        public Group Parent { get; set; }
        public int ParentID { get; set; }
        [Display (Name = "בחירת קבוצה")]
        // קבוצה חדשה
        public Group Group { get; set; }  
        [Display(Name = "הוספת תמונה")]
        public IFormFile File { get; set; }
        public Item Item { get; set; }
        public Price Price { get; set; }   
     
        [Display (Name = "הוספת תמונה לפריט")]
        public IFormFile FileItem { get; set; }

    }
}
