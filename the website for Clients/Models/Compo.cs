using System.ComponentModel;

namespace ClientAqttan.Models
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
    //public class CompoDto
    //{


    //    public string? Name { get; set; }

    //    public int ItemAmount { get; set; }
    //    public IFormFile? Image { get; set; }
    //}
}
