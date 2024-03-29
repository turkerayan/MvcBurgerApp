using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MVCGrup2.Areas.Customer.Models;
using MVCGrup2.Enums;
using System.ComponentModel.DataAnnotations;

namespace MVCGrup2.Areas.Admin.Models
{
    public class ExtraMatViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        [EnumDataType(typeof(Size))]

        public Size Size { get; set; }
		//[NotMapped]
		[ValidateNever]
		public IFormFile Image { get; set; }
        [ValidateNever]
        public string ImagePath { get; set; }

        //public string OrderId { get; set; }
        [ValidateNever]

        public ICollection<OrderViewModel> OrdersViewModel { get; set; }

        //public string MenuId { get; set; }

        //public ICollection<Menu> Menus { get; set; }

        private int _extraCount { get; set; }

        public int ExtraCount
        {
            get { return _extraCount; }
            set { _extraCount = value < 0 ? 0 : value; }
        }
        private double _price;

        public double Price
        {
            get { return _price; }
            set
            {
                _price = value;

                switch (Size)
                {
                    case Size.Medium:
                        _price *= 1.1d;
                        break;
                    case Size.Large:
                        _price *= 1.2d;
                        break;
                    case Size.XLarge:
                        _price *= 1.3d;
                        break;
                }
                if (ExtraCount > 0)
                {
                    _price = _price * ExtraCount;
                }
                else
                    _price = _price * 1;

			}
        }
    }
}
