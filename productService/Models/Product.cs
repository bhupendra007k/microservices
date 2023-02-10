using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace productservice.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        
        public Guid ProductCategoryId { get; set; }
        public virtual ProductCategory Category { get; set; }

        public Guid DiscountId { get; set; }
        public  Discount Discount { get; set; }
     
       


    }
}
