using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Models
{
    public class OrderLookupResponse : Response
    {
        public List<Order> OrderList { get; set; } //we'll return an order object as well as the properties it inherits
    }
}
