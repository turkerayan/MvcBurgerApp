﻿using MVCGrup2.Entities.Abstract;
using MVCGrup2.Enums;

namespace MVCGrup2.Entities.Concrete
{
    public class ExtraMat : Product
    {
        public ExtraMat(string name, double price, string description, bool active, Size size,string ımageName) : base(name, price, description, active, size, ımageName  )
        {
            
        }
        //public bool IsActive { get; set; }

    }
}
