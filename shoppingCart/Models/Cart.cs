using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shoppingCart.Models
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }
        public string UserCart { get; set; }

        //Navigation property
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid ProductId { get; set; }
        public virtual ICollection<Product> Products { get; set; }


    }
}
