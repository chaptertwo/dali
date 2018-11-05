using Myth.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myth.UI.Models
{
    public class FootprintVM
    {
        public IEnumerable<Footprint> Footprints { get; set; }
        public IEnumerable<Creature> Creatures { get; set; }
        public IEnumerable<Trait> Traits { get; set; }
        public IEnumerable<CreatureType> Types { get; set; }
    }
}