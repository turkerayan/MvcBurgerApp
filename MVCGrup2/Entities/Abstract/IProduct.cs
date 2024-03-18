using MVCGrup2.Enums;

namespace MVCGrup2.Entities.Abstract
{
    public interface IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }

        public Size Size { get; set; }
    }
}
