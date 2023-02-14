using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace productservice.Models
{
    public class Discount
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double DiscountPercent { get; set; }
        public bool Active { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
