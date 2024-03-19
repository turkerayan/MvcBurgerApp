using MVCGrup2.Data;

namespace MVCGrup2.Entities.Concrete
{
    public class Cart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int MVCGrup2UserId { get; set; }
        public MVCGrup2User User { get; set; }
    }
}
