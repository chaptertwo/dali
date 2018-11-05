using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTAdventure.Models
{
    public class ReturnJSONObject
    {
        public Scene Scene { get; set; }
        public Player Player { get; set; }
        public EventChoice EventChoice { get; set; }
        public Outcome Outcome { get; set; }
        public PlayerCharacter PlayerCharacter { get; set; }
        public Ending Ending { get; set; }
        public bool IsEnding { get; set; }
        public bool IsValidGame { get; set; }
    }
}
