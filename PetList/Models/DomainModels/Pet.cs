using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Models
{
    public partial class Pet
    {
        public int PetId { get; set; }

        [Required(ErrorMessage = "Please enter your pet's name.")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Range(0.0, 100000.0, ErrorMessage = "Weight must be more than 0.")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Please specify the type of pet.")]
        public string ClassificationId { get; set; }

        public Classification Classification { get; set; }
        public ICollection<PetOwner> PetOwners { get; set; }
    }
}
