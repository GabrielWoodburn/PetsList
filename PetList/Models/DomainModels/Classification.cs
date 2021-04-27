using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetList.Models
{
    public class Classification
    {
        [MaxLength(10)]
        [Required(ErrorMessage = "Please enter a pet classification id.")]
        [Remote("CheckClassification", "Validation", "Admin")]
        public string ClassificationId { get; set; }

        [StringLength(25)]
        [Required(ErrorMessage = "Please enter a classification name.")]
        public string Name { get; set; }

        public ICollection<Pet> Pets { get; set; }
    }
}
