using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Models
{
    public class AddOrderResponse : Response
    {
        public Order order { get; set; }
        public List<Order> orders { get; set; } //may need to create a new day???
    }
}
