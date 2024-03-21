using Microsoft.AspNetCore.Http.Metadata;
using MVCGrup2.Data;
using MVCGrup2.Entities.Abstract;
using MVCGrup2.Enums;
using System.ComponentModel.DataAnnotations;

namespace MVCGrup2.Entities.Concrete
{
    public class Menu : Product
    {
        
        public Menu(string name, double price, string description, bool active, Size size, string mageName) : base(name, price, description, active, size, mageName)
        {
        }

        private int _menuCount { get; set; }
        public int MenuCount
        {
            get { return _menuCount; }
            set { _menuCount = (value < 0) ? 0 : value; }
        }

        public string? ExtraMatId { get; set; }

        public ICollection<ExtraMat>? ExtraMats { get; set; }

        public string? OrderId { get; set; }

        public ICollection<Order>? Orders { get; set; }
     


     

    }
}
