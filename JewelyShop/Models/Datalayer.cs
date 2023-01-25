using System.Data.Entity;

namespace JewelyShop.Models
{
    public class Datalayer:DbContext
    {
        private static Datalayer data;
        
        private Datalayer():base("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Jewelry;Data Source=MSI\\SQLEXPRESS") 
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<Datalayer>());
            
            if (Groups.Count() == 0) Seed();
        }
        public static Datalayer Data { get 
            {
                if (data == null) data = new Datalayer();
                return data; } }
        private void Seed()
        {
            Group group = new Group { Name = "חנות תכשיטים" };
            Groups.Add(group);

            Price price = new Price { MyPrice = 150000, End = DateTime.Now.AddYears(20) };
            Prices.Add(price);

            SaveChanges();
        }
        // מאפיין המחזיר את כל הקבוצות עם הטעינה שלהם
        public List<Group> GroupsAllIncluded
        {
            get
            {
                List<Item> items = Data.Items.Include(i=>i.Prices).Include(i=>i.Images).Include(i=>i.Group).ToList();
                return data.Groups.Include(g=>g.Items).Include(g=>g.SubGroups).ToList();
            }
        }


        public DbSet<Group> Groups { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
