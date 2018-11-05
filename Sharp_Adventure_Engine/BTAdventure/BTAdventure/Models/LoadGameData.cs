using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTAdventure.Models
{
    public class LoadGameData
    {
        public IEnumerable<PlayerCharacter> PlayerCharacters { get; set; }
        public IEnumerable<Game> Games { get; set; }
        public string UserId { get; set; }
    }
}
