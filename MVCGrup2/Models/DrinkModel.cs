using MVCGrup2.Enums;
using System.ComponentModel.DataAnnotations;

namespace MVCGrup2.Models
{
    public class DrinkModel
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        [EnumDataType(typeof(Size))]
        public Size Size { get; set; }
        public IFormFile? Image { get; set; }
    }   
}
