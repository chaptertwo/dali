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
    public class CreateCreatureVM : IValidatableObject
    {
        public IEnumerable<Creature> Creatures { get; set; }
        public Creature Creature { get; set; }
        [DisplayName("Nest")]
        public IEnumerable<Nest> Nests { get; set; }
        public Region Region { get; set; }
        [DisplayName("Trait")]
        public IEnumerable<Trait> Traits { get; set; }
        [DisplayName("Type")]
        public IEnumerable<CreatureType> Types { get; set; }

        public List<TraitVM> TraitsSelect { get; set; }
        //public CreateCreatureVM(Creature creature)
        //{
        //    Creature = creature;
        //    TraitVM trait = N
        //}

        public int SelectTypeId { get; set; }
        public IEnumerable<SelectListItem> TypeList
        {
            get
            {
                return new SelectList(Types, "TypeId", "TypeName");
            }
            set { }
        }

        public int SelectNestId { get; set; }
        public IEnumerable<SelectListItem> NestList
        {
            get { return new SelectList(Nests, "NestId", "NestName"); }
            set { }
        }

        public int SelectTraitId { get; set; }
        public IEnumerable<SelectListItem> TraitsList
        {
            get { return new SelectList(Traits, "TraitId", "TraitName"); }
            set { }
        }
        public int SelectTraitIdTwo { get; set; }
        public IEnumerable<SelectListItem> TraitsListTwo
        {
            get { return new SelectList(Traits, "TraitId", "TraitName"); }
            set { }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validation)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            if(TraitsSelect.All(a => a.IsSelected == false))
            {
                errors.Add(new ValidationResult($"Atleast one Trait must be selected."));
            }

            return errors;
        }
       
    }
}