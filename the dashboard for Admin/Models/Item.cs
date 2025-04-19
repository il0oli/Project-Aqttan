using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace AQTTan.Models
{
    public class Item
    {
        public int Id { get; set; }
        [DisplayName("الأسم")]
        public string? Name { get; set; }
        [DisplayName("الوصف")]
        public string? Description { get; set; }
        [DisplayName("السعر")]
        public decimal? Price { get; set; }
        [DisplayName("الصورة")]
        public byte[]? Image { get; set; }

        public Compo? Compo { get; set; }
        [ForeignKey("CompoId")]
        [DisplayName("المكون")]
        public int CompoId { get; set; }
    }
}
