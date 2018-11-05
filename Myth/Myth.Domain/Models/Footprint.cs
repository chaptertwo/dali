using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myth.Domain.Models
{
    public class Footprint
    {
        //[Column("FootprintId")]
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FootprintId { get; set; }

        //[ForeignKey("CreatureId")]
        public int CreatureId { get; set; }
        public decimal FootprintLat { get; set; }
        public decimal FootprintLong { get; set; }
        public DateTime? FootprintDate { get; set; }
        public string FootprintType { get; set; }
        public bool IsClicked { get; set; }
        //public virtual Creature Creature { get; set; }
    }
}
