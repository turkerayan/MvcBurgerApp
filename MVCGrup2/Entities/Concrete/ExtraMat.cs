using MVCGrup2.Entities.Abstract;
using MVCGrup2.Enums;

namespace MVCGrup2.Entities.Concrete
{
    public class ExtraMat : Product
    {
        public ExtraMat(string name, double price, string description, bool active, Size size, string? mageName) : base(name, price, description, active, size, mageName)
        {
        }
        //public bool IsActive { get; set; }
        //public string OrderId { get; set; }

        public ICollection<Order> Orders { get; set; }

        //public string MenuId { get; set; }

        //public ICollection<Menu> Menus { get; set; }

        public int ExtraCount { get; set; }

      
    }
}
