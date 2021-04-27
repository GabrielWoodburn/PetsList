using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Models
{
    public class PetDTO
    {
        public int PetId { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public Dictionary<int, string> Owners { get; set; }

        public void Load(Pet pet)
        {
            PetId = pet.PetId;
            Name = pet.Name;
            Weight = pet.Weight;
            Owners = new Dictionary<int, string>();
            foreach (PetOwner po in pet.PetOwners)
            {
                Owners.Add(po.Owner.OwnerId, po.Owner.FullName);
            }
        }
    }
}
