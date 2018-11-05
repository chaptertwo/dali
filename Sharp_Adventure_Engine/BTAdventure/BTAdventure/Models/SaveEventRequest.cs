using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTAdventure.Models
{
    public class SaveEventRequest
    {
        public int SceneId { get; set; }
        public int EventId { get; set; }
        public int? GenerationNumber { get; set; }
        public string EventName { get; set; }
        public string StartText { get; set; }
        public string ImgURL { get; set; }
        public string PositiveText { get; set; }
        public string NegativeText { get; set; }
        public string PositiveButton { get; set; }
        public string NegativeButton { get; set; }
        public int? PositiveRoute { get; set; }
        public int? NegativeRoute { get; set; }
        public int? PositiveSceneRoute { get; set; }
        public int? NegativeSceneRoute { get; set; }
        public int? PositiveEndingId { get; set; }
        public int? NegativeEndingId { get; set; }
        public int PositiveOutcomeId { get; set; }
        public int NegativeOutcomeId { get; set; }
        public int PositiveHealth { get; set; }
        public int PositiveGold { get; set; }
        public int NegativeHealth { get; set; }
        public int NegativeGold { get; set; }
    }
}
