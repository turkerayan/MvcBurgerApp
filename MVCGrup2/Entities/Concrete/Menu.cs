using Microsoft.AspNetCore.Http.Metadata;
using MVCGrup2.Data;
using MVCGrup2.Entities.Abstract;
using MVCGrup2.Enums;
using System.ComponentModel.DataAnnotations;

namespace MVCGrup2.Entities.Concrete
{
    public class Menu : Product
    {
        
        //public Menu(string name, double price, string description, bool active, Size size, string imagename) : base(name, price, description, active, size, imagename)
        //{
        //}
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        [EnumDataType(typeof(Size))]
        public Size Size { get; set; }
        public string? ImageName { get; set; }
        private int _menuCount { get; set; }
        public int MenuCount
        {
            get { return _menuCount; }
            set { _menuCount = (value < 0) ? 0 : value; }
        }

        //public string? ExtraMatId { get; set; }

        //public ICollection<ExtraMat>? ExtraMats { get; set; }

        //public string? OrderId { get; set; }

        public ICollection<Order> Orders { get; set; }
     


     

    }
}
