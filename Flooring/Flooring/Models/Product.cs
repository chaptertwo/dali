using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Models
{
    public class Product
    {
        public string ID { get; set; }
        public string ProductType { get; set; }
        public decimal CostPerSQFoot { get; set; }
        public decimal LaborCostPerSQFoot { get; set; }
    }
}
