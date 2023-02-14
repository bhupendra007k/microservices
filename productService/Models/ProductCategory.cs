using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace productservice.Models
{
    public class ProductCategory
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //Navigation Property
        public ICollection<Product> Products { get; set; }



    }
}
