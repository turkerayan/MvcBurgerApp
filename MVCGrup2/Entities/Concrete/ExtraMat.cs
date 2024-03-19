using MVCGrup2.Entities.Abstract;
using MVCGrup2.Enums;

namespace MVCGrup2.Entities.Concrete
{
    public class ExtraMat : Product
    {
        public ExtraMat(string name, double price, string description, bool active, Size size) : base(name, price, description, active, size)
        {
            
        }
        //public bool IsActive { get; set; }
        public ICollection<Cart> Carts { get; set; }

    }
}
