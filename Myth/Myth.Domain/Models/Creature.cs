using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myth.Domain.Models
{
    public class Creature
    {
        [DisplayName("Name")]
        [Required]
        public string CreatureName { get; set; }
        public int CreatureId { get; set; }
        //public int RegionId { get; set; }
        public Nest Nest { get; set; }
        public int NestId { get; set; }
        public int TypeId { get; set; }
        public CreatureType Type { get; set; }
        public IEnumerable<Trait> Traits { get; set; } = new List<Trait>();
        public IEnumerable<Footprint> Footprints { get; set; }
        public int TraitId { get; set; }
        [DisplayName("Picture URL")]
        public string Picture { get; set; }
        public decimal CreatureLat { get; set; }
        public decimal CreatureLong { get; set; }
        [DisplayName("Description")]
        public string CreatureDescription { get; set; }
        public bool CreatureIsRevealed { get; set; }
        public bool CreatureHasNest { get; set; }
        public bool CreatureIsPlaced { get; set; }
    }
}
