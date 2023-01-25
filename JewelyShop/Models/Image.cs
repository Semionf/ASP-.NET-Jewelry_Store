using System.ComponentModel.DataAnnotations;

namespace JewelyShop.Models
{
    public class Image
    {
        public Image() { }
        [Key]
        public int ID { get; set; }
        public Item Item { get; set; }
        public byte[] MyImage { get; set; }
        public IFormFile SetImage
        {
            set
            {
                if (value == null) return;
                MemoryStream stream = new MemoryStream();
                value.CopyTo(stream);
                MyImage = stream.ToArray();
            }
        }
    }
}