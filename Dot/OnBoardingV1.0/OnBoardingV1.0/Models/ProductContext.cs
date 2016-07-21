using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace OnBoardingV1._0.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base("OnBoardingV1._0")
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<CartItem> ShoppingCartItems { get; set; }
    }
}
