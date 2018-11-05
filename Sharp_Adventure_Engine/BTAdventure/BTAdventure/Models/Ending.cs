using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTAdventure.Models
{
    public class Ending
    {
        public int EndingId { get; set; }
        public int GameId { get; set; }
        [DisplayName("Name of Ending")]
        public string EndingName { get; set; }
        [DisplayName("Ending Text")]
        public string EndingText { get; set; }
    }
}
