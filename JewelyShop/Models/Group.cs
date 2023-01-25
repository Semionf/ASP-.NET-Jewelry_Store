﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace JewelyShop.Models
{
    public class Group
    {
        public Group()
        {
            SubGroups = new List<Group>();
            Items = new List<Item>();
        }
        [Key]
        public int ID { get; set; }
        [Required, Display(Name ="שם קבוצה")]
        public string Name { get; set; }
        [Display(Name ="תיאור")]
        public string Description { get; set; }
        [Display(Name ="תמונה")]
        public byte[] Image { get; set; }
        public Group Parent { get; set; }
        public List<Group> SubGroups { get; set; }

        public List<Item> Items { get; set; }

        // פונקציה של הוספת תת קבוצה
        public Group AddSubGroup(string name)
        {
            Group group = new Group { Name = name, Parent = this };
            return AddSubGroup(group);
        }
        public Group AddSubGroup(string name, string description)
        {
            Group group = new Group { Name = name, Description = description, Parent = this };
            return AddSubGroup(group);
        } 
        public Group AddSubGroup(string name, string description, IFormFile image)
        {
            Group group = new Group { Name = name, Description = description, SetImage = image, Parent = this };
            return AddSubGroup(group);
        } 
        public Group AddSubGroup(Group subGroup)
        {
            subGroup.Parent= this;
            SubGroups.Add(subGroup);
            return subGroup;
        }
        
        // פונקציה של הוספת פריט
 
        public Item AddItem(string name, string description, decimal price)
        {
             Item item = new Item { Name = name, Description = description, Group=this  };
            item.AddPrice(price);
            return AddItem(item);
        }
        public Item AddItem(string name, string description,  decimal price, IFormFile image)
        {
             Item item = new Item { Name = name, Description = description, Group = this };
            item.AddPrice(price);
            item.AddImage(image);
            return AddItem(item);
        }
        public Item AddItem(string name, string description, decimal price , List<IFormFile> images)
        {
            Item item = new Item { Name = name, Description = description, Group = this };
            item.AddPrice(price);
            foreach (IFormFile image in images)
            {
                item.AddImage(image);
            }
            return AddItem(item);
        }
        public Item AddItem(string name, string description, decimal price, List<IFormFile> images, DateTime start, DateTime end)
        {
            Item item = new Item { Name = name, Description = description, Group = this };
            item.AddPrice(price, start, end);
            foreach (IFormFile image in images)
            {
                item.AddImage(image);
            }
            
            return AddItem(item);
        }
        public Item AddItem(Item item)
        {
            item.Group = this;
            Items.Add(item);
            return item;
        }

        // פונקציה של הכנסת תמונה
        public IFormFile SetImage 
        {
            set 
            { 
                if (value == null) return;
                MemoryStream stream = new MemoryStream();
                value.CopyTo(stream);
                Image = stream.ToArray();
            } 
        }

        // הצגת פריטים כולל תתי קבוצות
        [NotMapped]
        public List<Item> AllItems
        {
            get { return GetItems(this); }
        }

        private List<Item> GetItems(Group group)
        {
           List<Item> items = new List<Item>();
            return GetItems(group, items);
        }
        private List<Item> GetItems(Group group, List<Item> items)
        {
           if(group.SubGroups.Count > 0)
                foreach (Group group1 in group.SubGroups)
                {
                    return GetItems(group1, items);
                }
           if(group.Items.Count > 0)
                foreach (Item item in group.Items)
                {
                    items.Add(item);
                }
           return items;
        }
    }
}