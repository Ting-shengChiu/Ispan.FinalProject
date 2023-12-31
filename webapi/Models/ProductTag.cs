﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace webapi.Models
{
    public partial class ProductTag
    {
        public ProductTag()
        {
            ActivityDiscounts = new HashSet<ActivityDiscount>();
            Coupons = new HashSet<Coupon>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public int ProductTagCategoryId { get; set; }
        public string Name { get; set; }

        public virtual ProductTagCategory ProductTagCategory { get; set; }

        public virtual ICollection<ActivityDiscount> ActivityDiscounts { get; set; }
        public virtual ICollection<Coupon> Coupons { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}