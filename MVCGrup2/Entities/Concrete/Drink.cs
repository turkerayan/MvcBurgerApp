using MVCGrup2.Entities.Abstract;
using MVCGrup2.Enums;

namespace MVCGrup2.Entities.Concrete
{
    public class Drink : Product
    {
        public Drink(string name, double price, string description, bool active, Size size, string? imageName) : base(name, price, description, active, size, imageName)
        {
        }
        //public bool IsActive { get; set; }

    }
}
