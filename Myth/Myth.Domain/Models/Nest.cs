using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myth.Domain.Models
{
    public class Nest : IValidatableObject
    {
        public int NestId { get; set; }
        public int RegionId { get; set; }
        [DisplayName("Nest Latitude")]
        [Required]
        [Range(-90, 90)]
        public decimal NestLat { get; set; }
        [DisplayName("Nest Longitude")]
        [Required]
        [Range(-180, 180)]
        public decimal NestLong { get; set; }
        [DisplayName("Nest Name")]
        public string NestName { get; set; }
        public bool IsPlaced { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            var minimumLat = -90;
            var maximumLat = 90;
            var minLong = -180;
            var maxLong = 180;
            if (NestLat > maximumLat || NestLat < minimumLat || NestLong > maxLong | NestLong < minLong)
            {
                errors.Add(new ValidationResult($"Valid coordinates are between -90 and 90 for latitude and -180 and 180 for longitude. Please try again."));
            }
            return errors;
        }
    }
}
