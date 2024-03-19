using MVCGrup2.Entities.Abstract;
using MVCGrup2.Enums;

namespace MVCGrup2.Entities.Concrete
{
    public class Burger : Product
    {
        public Burger(string name, double price, string description, bool active, Size size,string? ımageName) : base(name, price, description, active, size, ımageName)
        {
            
        }

        //public bool IsActive { get; set; }
    }
}
