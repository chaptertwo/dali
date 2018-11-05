using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTAdventure.Models
{
    public class PlayerCharacter
    {
        public int CharacterId { get; set; }
        public string PlayerId { get; set; }
        public int SceneId { get; set; }
        public int EventChoiceId { get; set; }
        public string CharacterName { get; set; }
        public int HealthPoints { get; set; }
        public int Gold { get; set; }
    }
}
