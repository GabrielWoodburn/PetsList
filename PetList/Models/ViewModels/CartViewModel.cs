using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Models
{
    public class CartViewModel
    {
        public IEnumerable<PetItem> List { get; set; }
        public double TotalWeight { get; set; }
        public RouteDictionary PetGridRoute { get; set; }
    }
}
