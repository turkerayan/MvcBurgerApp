using Microsoft.AspNetCore.Http.Metadata;
using MVCGrup2.Data;
using MVCGrup2.Entities.Abstract;
using MVCGrup2.Enums;
using System.ComponentModel.DataAnnotations;

namespace MVCGrup2.Entities.Concrete
{
    public class Menu : Product
    {
        
        public Menu(string name, double price, string description, bool active, Size size, string imagename) : base(name, price, description, active, size, imagename)
        {
        }


        private int _menuCount { get; set; }
        public int MenuCount
        {
            get { return _menuCount; }
            set { _menuCount = (value < 0) ? 0 : value; }
        }

        public int? ExtraMatId { get; set; }

        public ICollection<ExtraMat>? ExtraMats { get; set; } = new List<ExtraMat>();

        public Order Order { get; set; }


        public double MenuPrice
        {
            get { return MenuPrice; }
            set
            {
                MenuPrice += MenuPrice * _menuCount;
                switch (Size)
                {

                    case Size.Medium:
                        MenuPrice += MenuPrice * 0.1;
                        break;
                    case Size.Large:
                        MenuPrice += MenuPrice * 0.2;
                        break;
                    case Size.XLarge:
                        MenuPrice += MenuPrice * 0.3;
                        break;
                    case Size.Small:
                        break;
                    default:
                        break;
                }

                if (ExtraMats != null)
                {
                    foreach (var item in ExtraMats)
                    {
                        MenuPrice += item.Price;
                    }
                }


            }
        }

    }
}
