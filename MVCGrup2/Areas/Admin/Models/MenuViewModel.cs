using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using MVCGrup2.Areas.Customer.Models;
using MVCGrup2.Entities.Concrete;
using MVCGrup2.Enums;
using System.ComponentModel.DataAnnotations;


namespace MVCGrup2.Areas.Admin.Models
{
    public class MenuViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Active { get; set; }

        [EnumDataType(typeof(Size))]

        public Size Size { get; set; }

		[ValidateNever]

		public IFormFile Image { get; set; }
        [ValidateNever]
        public string ImagePath { get; set; }

        public int MenuCount { get; set; }

        [ValidateNever]

        public ICollection<OrderViewModel> OrdersViewModel { get; set; }

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
                _price = _price * 1;
            }
        }
    }
}

