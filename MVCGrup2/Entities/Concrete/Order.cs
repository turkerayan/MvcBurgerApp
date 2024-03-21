using MVCGrup2.Data;
using MVCGrup2.Entities.Abstract;
using MVCGrup2.Enums;
using System.ComponentModel.DataAnnotations;

namespace MVCGrup2.Entities.Concrete
{
    public class Order
    {
      
        public Order()
        {
            Total = 0;
        }

        public Guid Id { get; set; }

        public double Total { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public Size Size { get; set; }

        public string MenuId { get; set; }

        public ICollection<Menu> Menus { get; set; } = new List<Menu>();

        public void CalcTotal()
        {
            if (Menus != null)
            {
                switch (Size)
                {

                    case Size.Medium:
                        Total += Total * 0.1;
                        break;
                    case Size.Large:
                        Total += Total * 0.2;
                        break;
                    case Size.XLarge:
                        Total += Total * 0.3;
                        break;
                    case Size.Small:
                        break;
                    default:
                        break;
                }
                Total = 0;
                foreach(var menu in Menus) 
                {
                    Total += menu.MenuPrice;
                
                }






            }
        }

    }
}
