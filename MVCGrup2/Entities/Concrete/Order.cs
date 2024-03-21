﻿using MVCGrup2.Data;
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

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public int OrderCount { get; set; }

        //public string? MenuId { get; set; }

        public ICollection<Menu> Menus { get; set; }

        //public string? ExtraMatId { get; set; }

        public ICollection<ExtraMat> ExtraMats { get; set; }

        public double Total { get; set; }

        public MVCGrup2User User { get; set; }

    }
}
