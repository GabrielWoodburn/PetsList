using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Models
{
    public class PetOwner
    {
        public int PetId { get; set; }
        public int OwnerId { get; set; }

        public Owner Owner { get; set; }
        public Pet Pet { get; set; }
    }
}
