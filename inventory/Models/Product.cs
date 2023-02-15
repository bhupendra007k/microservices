using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace inventory.Models
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
        public Guid InventoryId { get; set; }
        public virtual Inventory Inventory { get; set; }

        //Navigation Properties
       /* public Guid? ProductCategoryId { get; set; }
        public virtual ProductCategory Category { get; set; }

        public Guid DiscountId { get; set; }
        public virtual Discount Discount { get; set; }*/

    }
}
