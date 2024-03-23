using MVCGrup2.Entities.Abstract;
using MVCGrup2.Enums;
using System.ComponentModel.DataAnnotations;

namespace MVCGrup2.Entities.Concrete
{
    public class ExtraMat : BaseEntity
    {
        public ExtraMat(Guid id, string name, double price, string description, bool active, Size size, string? pictureName) : base(id,name, price, description, active, size, pictureName)
        {
        }

        //public Guid Id { get; set; }
        //public string Name { get; set; }
        //public double Price { get; set; }
        //public string Description { get; set; }
        //public bool Active { get; set; }
        //[EnumDataType(typeof(Size))]
        //public Size Size { get; set; }
        //public string? PictureName { get; set; }
        public ICollection<Order>? Orders { get; set; }

        public int ExtraCount { get; set; }

      

        //public bool IsActive { get; set; }
        //public string OrderId { get; set; }
        //public string MenuId { get; set; }

        //public ICollection<Menu> Menus { get; set; }
    }
}
