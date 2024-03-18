﻿using MVCGrup2.Enums;
using System.ComponentModel.DataAnnotations;

namespace MVCGrup2.Entities.Abstract
{
    public abstract class Product : IProduct
    {
        protected Product(string name, double price, string description, bool active, Size size)
        {
            Name = name;
            Price = price;
            Description = description;
            Active = active;
            Size = size;

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        [EnumDataType(typeof(Size))]
        public Size Size { get; set; }
    }
}