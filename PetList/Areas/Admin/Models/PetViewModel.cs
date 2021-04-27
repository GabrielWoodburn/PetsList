using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Models
{
    public class PetViewModel : IValidatableObject
    {
        public Pet Pet { get; set; }
        public IEnumerable<Classification> Classifications { get; set; }
        public IEnumerable<Owner> Owners { get; set; }
        public int[] SelectedOwners { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext ctx)
        {
            if (SelectedOwners == null)
            {
                yield return new ValidationResult(
                  "Please select at least one owner.",
                  new[] { nameof(SelectedOwners) });
            }
        }
    }
}
