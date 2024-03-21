using Microsoft.AspNetCore.Http;
using MVCGrup2.Entities.Concrete;
using MVCGrup2.Enums;


namespace MVCGrup2.Models
{
    public class MenuViewModel
    {
        public Size Size { get; set; }

        public int ExtraMatId { get; set; }

        public ICollection<ExtraMat> ExtraMats { get; set; } = new List<ExtraMat>();

        public int MenuCount { get; set; }

        public Order Order { get; set; }

        public IFormFile Image { get; set; }
    }
}
