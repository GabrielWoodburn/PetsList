using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Models
{
    public interface IPetItem
    {
        Repository<Pet> Pets { get; }
        Repository<Owner> Owners { get; }
        Repository<PetOwner> PetOwners { get; }
        Repository<Classification> Classifications { get; }

        void DeleteCurrentPetOwners(Pet pet);
        void LoadNewPetOwners(Pet pet, int[] ownerids);
        void Save();
    }
}
