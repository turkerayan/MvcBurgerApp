using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MVCGrup2.Data;
using MVCGrup2.Entities.Concrete;
using MVCGrup2.Enums;

namespace MVCGrup2.Areas.Customer.Models
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public OrderStatus OrderStatus { get; set; }

        public int OrderCount { get; set; }

        //public string? MenuId { get; set; }
        [ValidateNever]

        public ICollection<Menu> Menus { get; set; } = new List<Menu>();

        //public string? ExtraMatId { get; set; }
        [ValidateNever]
        public ICollection<ExtraMat> ExtraMats { get; set; } = new List<ExtraMat>();

        public MVCGrup2User User { get; set; }

        public double Total { get; set; }

        public double TotalCalc()
        {

            foreach (var item in Menus)
            {
                Total += item.Price;
            }

            foreach (var item in ExtraMats)
            {
                Total += item.Price;
            }

            Total = Total * OrderCount;

            return Total;
        }

    }
}
