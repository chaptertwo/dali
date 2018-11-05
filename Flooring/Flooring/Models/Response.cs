using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flooring.Models
{
    public class Response //all responses will inherit these properties
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
