using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTAdventure.Models
{
    public class EventChoice
    {
        public int EventChoiceId { get; set; }
        public int SceneId { get; set; }
        public int? GenerationNumber { get; set; }
        public string ImgUrl { get; set; }
        public string EventName { get; set; }
        public string StartText { get; set; }
        public string PositiveText { get; set; }
        public string NegativeText { get; set; }
        public int? PositiveRoute { get; set; }
        public int? NegativeRoute { get; set; }
        public string PositiveButton { get; set; }
        public string NegativeButton { get; set; }
        public int? PositiveSceneRoute { get; set; }
        public int? NegativeSceneRoute { get; set; }
        public int? PositiveEndingId { get; set; }
        public int? NegativeEndingId { get; set; }
    }
}
