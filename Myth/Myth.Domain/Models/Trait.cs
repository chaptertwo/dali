using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myth.Domain.Models
{
    public class Trait
    {
        
        public int TraitId { get; set; }
        [DisplayName("Trait")]
        public string TraitName { get; set; }
        [DisplayName("Trait Description")]
        public string TraitDescription { get; set; }
    }
}
