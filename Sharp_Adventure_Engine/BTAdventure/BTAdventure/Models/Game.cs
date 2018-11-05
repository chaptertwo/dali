using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTAdventure.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public string GameTitle { get; set; }
        public string IntroText { get; set; }
        public int Health { get; set; }
        public int Gold { get; set; }
    }
}
