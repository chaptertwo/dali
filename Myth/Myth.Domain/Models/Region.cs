using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myth.Domain.Models
{
    public class Region
    {
        public int RegionId { get; set; }
        public string CountryAbbr { get; set; }
        public string CountryFull { get; set; }
        public decimal RegionLat { get; set; }
        public decimal RegionLong { get; set; }
    }
}
