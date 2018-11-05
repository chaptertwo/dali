using BTAdventure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTAdventure.UI.Models
{
    public class EditGenerationVM
    {
        public IEnumerable<EventChoice> AllEventChoice { get; set; }
        public IEnumerable<Scene> AllScene { get; set; }
        public IEnumerable<EventChoice> AllEventByScene { get; set; }
    }
}