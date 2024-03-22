using MVCGrup2.Entities.Concrete;

namespace MVCGrup2.Models
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public int OrderCount { get; set; }

        //public string? MenuId { get; set; }

        public ICollection<Menu>? Menus { get; set; }

        //public string? ExtraMatId { get; set; }

        public ICollection<ExtraMat>? ExtraMats { get; set; }

        public double Total
        {
            get { return Total; }
            set
            {
                if (Menus != null)
                {
                    foreach (var item in Menus)
                    {
                        Total += item.Price;
                    }
                }

                if (ExtraMats != null)
                {
                    foreach (var item in ExtraMats)
                    {
                        Total += item.Price;
                    }
                }
                Total *= OrderCount;
            }
        }
    }
}
