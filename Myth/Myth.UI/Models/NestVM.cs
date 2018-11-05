using Myth.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Myth.UI.Models
{
    public class NestVM
    {
        public Creature Creature { get; set; }
        public bool IsSelected { get; set; }
    }
}