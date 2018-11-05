using Myth.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Myth.UI.Models
{
    public class TraitVM
    {
        [Required]
        public Trait Trait { get; set; }
        public bool IsSelected { get; set; }
    }
}