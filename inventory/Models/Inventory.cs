using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace inventory.Models
{
    public class Inventory
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }

        //Navigation properties
        public virtual IList<Product> Products { get; set; }
    }
}
