using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTAdventure.Models
{
    public class EventCreationData
    {
        public EventChoice EventChoice { get; set; }
        public IEnumerable<EventChoice> AvailableChoices { get; set; }
        public IEnumerable<Scene> GameScenes { get; set; }
        public IEnumerable<Ending> GameEndings { get; set; }
        public Outcome PositiveOutcome { get; set; }
        public Outcome NegativeOutcome { get; set; }
        public int SceneId { get; set; }
    }
}
