using System.ComponentModel;

namespace AQTTan.Models
{
    public class Compo
    {
        public int Id { get; set; }
        [DisplayName("الأسم")]
        public string? Name { get; set; }
        [DisplayName("عدد الأصناف فيه")]
        public int ItemAmount { get; set; }
        [DisplayName("الصورة")]
        public byte[]? Image { get; set; }
    }
}
