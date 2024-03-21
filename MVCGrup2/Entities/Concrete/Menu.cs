using MVCGrup2.Data;
using MVCGrup2.Enums;
using System.ComponentModel.DataAnnotations;

namespace MVCGrup2.Entities.Concrete
{
    public class Menu
    {
        public Guid Id { get; set; }

        public Size Size { get; set; }

        public int ExtraMatId { get; set; }

        public ICollection<ExtraMat> ExtraMats { get; set; } = new List<ExtraMat>();

        [Required]
        public string MVCGrup2UserId { get; set; }
        [Required]
        public MVCGrup2User User { get; set; }

        public int MenuCount { get; set; }

        public Order Order { get; set; }

        public double MenuPrice { get { return MenuPrice; }
            set {
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
