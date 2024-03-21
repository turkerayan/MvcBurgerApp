using Microsoft.AspNetCore.Http;
using MVCGrup2.Entities.Abstract;
using MVCGrup2.Entities.Concrete;
using MVCGrup2.Enums;
using System.ComponentModel.DataAnnotations;

namespace MVCGrup2.Models
{
    public class ExtraMatViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        [EnumDataType(typeof(Size))]

        public Size Size { get; set; }

        public IFormFile Image { get; set; }

        //public string OrderId { get; set; }

        public ICollection<Order> Orders { get; set; }

        //public string MenuId { get; set; }

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
                Price += Price * ExtraCount;
            }
        }
    }
}
