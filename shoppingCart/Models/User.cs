using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace shoppingCart.Models
{
    public class User
    {

        [Key]
        public Guid Id { get; set; }
        public String Email { get; set; }
        public string UserName { get; set; }

        //Navigation
        public virtual Cart UserCart { get; set; }
    }
}
