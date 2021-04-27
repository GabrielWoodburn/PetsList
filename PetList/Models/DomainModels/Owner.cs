using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace PetList.Models
{
    public class Owner
    {
        public int OwnerId { get; set; }

        [Required(ErrorMessage = "Please enter a first name.")]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name.")]
        [MaxLength(100)]
        [Remote("CheckOwner", "Validation", "Admin", AdditionalFields = "FirstName, Operation")]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public ICollection<PetOwner> PetOwners { get; set; }
    }
}
