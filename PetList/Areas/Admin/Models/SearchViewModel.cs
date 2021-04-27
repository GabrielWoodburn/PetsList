using PetList.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Models
{
    public class SearchViewModel
    {
        public IEnumerable<Pet> Pets { get; set; }
        [Required(ErrorMessage = "Please enter a search term.")]
        public string SearchTerm { get; set; }
        public string Type { get; set; }
        public string Header { get; set; }
    }
}
