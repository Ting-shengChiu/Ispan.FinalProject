﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace webapi.Models
{
    public partial class DiscountType
    {
        public DiscountType()
        {
            ActivityDiscounts = new HashSet<ActivityDiscount>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ActivityDiscount> ActivityDiscounts { get; set; }
    }
}