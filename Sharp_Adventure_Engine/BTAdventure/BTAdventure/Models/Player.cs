using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTAdventure.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        [Required]
        public string PlayerName { get; set; }

    }
}
