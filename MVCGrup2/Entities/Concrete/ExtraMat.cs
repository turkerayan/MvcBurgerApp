using MVCGrup2.Entities.Abstract;
using MVCGrup2.Enums;

namespace MVCGrup2.Entities.Concrete
{
    public class ExtraMat : Product
    {
        public ExtraMat(string name, double price, string description, bool active, Size size, string? imageName) : base(name, price, description, active, size, imageName)
        {
        }
        //public bool IsActive { get; set; }

        public ICollection<Menu> Menus { get; set; }

        private int _extraCount { get; set; }
        public int ExtraCount
        {
            get { return _extraCount; }
            set { _extraCount = (value < 0) ? 0 : value; }
        }


        public double Price
        {
            get { return Price; }
            set
            {
                switch (Size)
                {

                    case Size.Medium:
                        Price += Price * 0.1;
                        break;
                    case Size.Large:
                        Price += Price * 0.2;
                        break;
                    case Size.XLarge:
                        Price += Price * 0.3;
                        break;
                    case Size.Small:
                        break;
                    default:
                        break;
                }
                Price += Price * _extraCount;



            }
        }
    }
}
