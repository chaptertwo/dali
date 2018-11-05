using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myth.Domain.Models
{
    public class CreatureType
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public string Species { get; set; }
        public string TypeDescription { get; set; }
        public string FootprintType { get; set; }
    }
}
