using MVCGrup2.Data;
using MVCGrup2.Entities.Abstract;

namespace MVCGrup2.Entities.Concrete
{
    public class Cart
    {

        public int Id { get; set; }

        public int BurgerId { get; set; }

        public ICollection<Burger> Burgers { get; set; }

        public int ExtraMatId { get; set; }

        public ICollection<ExtraMat> ExtraMats { get; set; }

        public int DrinkId { get; set; }

        public ICollection<Drink> Drinks { get; set; }

        public int MVCGrup2UserId { get; set; }

        public MVCGrup2User User { get; set; }

    }
}
