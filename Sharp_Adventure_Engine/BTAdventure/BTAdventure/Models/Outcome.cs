using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTAdventure.Models
{
    public class Outcome
    {
        public int OutcomeId { get; set; }
        public int LevelId { get; set; }
        public bool Positive { get; set; }
        public int Health { get; set; }
        public int Gold { get; set; }
        //hey rich, this is something I added since the database table had it and this model didn't -danny
        public int EventChoiceId { get; set; }
    }
}
