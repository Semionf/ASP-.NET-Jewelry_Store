using JewelyShop.Models;
using JewelyShop.ViewModels.Manager;
using Microsoft.AspNetCore.Mvc;
//Added by us
using System.Data.Entity;
using JewelyShop.ViewModels.Manager;

namespace JewelyShop.Controllers
{
    public class ManagerController : Controller
    {
        // הצגה של הקבוצות והפריטים של כל קבוצה
        public IActionResult Index(int? id)
        {
            if(id != null)
            {
                Group group = Datalayer.Data.GroupsAllIncluded.Find(g=>g.ID == id);
                if(group != null)
                return View(group);
            }
            Group group1 = Datalayer.Data.GroupsAllIncluded.First();
            return View(group1);
        }

        // הוספת קבוצה
        public IActionResult Create(int? id)
        {
            List<Group> groups = Datalayer.Data.Groups.ToList();
            if(id != null)
            {
                Group parent = groups.Find(g => g.ID == id);
                if (parent != null) return View(new VMCreateGroup { Groups = groups ,Parent = parent, ParentID = id.Value });
            }
            return View(new VMCreateGroup { Groups = groups, Parent = groups.First(), ParentID = groups.First().ID });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(VMCreateGroup VM)
        {
            Group parent = Datalayer.Data.Groups.FirstOrDefault(g => g.ID == VM.ParentID);
            if(parent != null)
            {
                // הוספת הקבוצה החדשה להורה
                parent.AddSubGroup(VM.Group);
                // הוספת התמונה לקבוצה החדשה
                VM.Group.SetImage = VM.File;
                
                if(VM.Item.Name != null)
                {
                    VM.Group.AddItem(VM.Item).AddPrice(VM.Price);
                    VM.Item.AddImage(VM.FileItem);
                }
               
                Datalayer.Data.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        // הוספת פריט
        public IActionResult AddItem(int? id)
        {
            List<Group> groups = Datalayer.Data.Groups.ToList();
            if (id != null)
            {
                Group group = groups.Find(g => g.ID == id);
                return View(new VMAddItem { Groups = groups, GroupID = id.Value, Group = group });
            }
            return RedirectToAction("Index","Home");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult AddItem(VMAddItem VM)
        {
            Group group = Datalayer.Data.Groups.FirstOrDefault(g => g.ID == VM.GroupID);
            if (group != null)
            {
                group.AddItem(VM.Item).AddPrice(VM.Price);
                group.Items.FirstOrDefault(i => i.ID == VM.Item.ID).AddImage(VM.File);
                
                Datalayer.Data.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Details(int? id)
        {
            // אם לא התקבל קוד מוצר, שולח לדף הראשי
            if (id == null) return RedirectToAction("Index", "Home");
            // שתי השורות מתחת זהות
            

            Item item = Datalayer.Data.Items.Include(i => i.Images).FirstOrDefault(f => f.ID == id);
            // אם הוא לא מצא את החבר
            if (item == null) return RedirectToAction("Index","Home");
            return View(new VMAddItem { Item = item });
        }
    }
}
