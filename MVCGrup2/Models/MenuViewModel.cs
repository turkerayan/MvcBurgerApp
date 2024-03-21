using Microsoft.AspNetCore.Http;
using MVCGrup2.Entities.Concrete;
using MVCGrup2.Enums;
using System.ComponentModel.DataAnnotations;


namespace MVCGrup2.Models
{
    public class MenuViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        [EnumDataType(typeof(Size))]

        public Size Size { get; set; }

      


        //public int ExtraMatId { get; set; }

        //public ICollection<ExtraMat> ExtraMats { get; set; } = new List<ExtraMat>();

        //public Order Order { get; set; }

        public IFormFile Image { get; set; }

        public int MenuCount { get; set; }

        //public string? OrderId { get; set; }

        public ICollection<Order>? Orders { get; set; }

        public double MenuPrice
        {
            get { return MenuPrice; }
            set
            {
                MenuPrice += MenuPrice * MenuCount;
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

                //if (ExtraMats != null)
                //{
                //    foreach (var item in ExtraMats)
                //    {
                //        MenuPrice += item.Price;
                //    }
                //}
            }
        }
    }
}
