using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shoppingCart.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ProductCategory { get; set; }
        public string? Sale { get; set; }
        public Guid CartId { get; set; }
        public virtual Cart UserCart { get; set; }
    }
}
