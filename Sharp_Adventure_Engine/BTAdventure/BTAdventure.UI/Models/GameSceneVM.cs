using BTAdventure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTAdventure.UI.Models
{
    public class GameSceneVM
    {
        public Scene Scene { get; set; }
        public Player Player { get; set; }
        public EventChoice EventChoice { get; set; }
        public Outcome Outcome { get; set; }
        public PlayerCharacter PlayerCharacter { get; set; }
        public Ending Ending { get; set; }
    }
}