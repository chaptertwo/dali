using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTAdventure.Models
{
    public class Scene
    {
        public int SceneId { get; set; }
        public int GameId { get; set; }
        public bool IsStart { get; set; }
        public string SceneName { get; set; }
    }
}
