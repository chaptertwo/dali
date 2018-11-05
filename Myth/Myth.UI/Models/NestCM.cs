using Myth.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Myth.UI.Models
{
    public class NestCM : IValidatableObject
    {
        public Nest Nest { get; set; }
        [DisplayName("Unassigned Creatures")]
        public IEnumerable<Creature> Creatures { get; set; }
        public int CreatureSelectedId { get; set; }
        public IEnumerable<Nest> Nests { get; set; }
        
        public List<NestVM> CreatureSelect { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validation)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if (CreatureSelect.All(a => a.IsSelected == false))
            {
                errors.Add(new ValidationResult($"Atleast one creature must be selected."));
            }

            return errors;
        }
    }
 


}