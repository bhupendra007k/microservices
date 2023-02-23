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
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string ProductCategory { get; set; }
        public string? Sale { get; set; }

        //Navigation Properties
        public Guid? ProductCategoryId { get; set; }
        public virtual ProductCategory Category { get; set; }

        public Guid DiscountId { get; set; }
        public virtual Discount Discount { get; set; }
     
       


    }
}
