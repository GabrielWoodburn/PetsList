using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Models
{
    public class PetItemUnit : IPetItem
    {
        private PetListContext context { get; set; }
        public PetItemUnit(PetListContext ctx) => context = ctx;

        private Repository<Pet> petData;
        public Repository<Pet> Pets
        {
            get
            {
                if (petData == null)
                    petData = new Repository<Pet>(context);
                return petData;
            }
        }

        private Repository<Owner> ownerData;
        public Repository<Owner> Owners
        {
            get
            {
                if (ownerData == null)
                    ownerData = new Repository<Owner>(context);
                return ownerData;
            }
        }

        private Repository<PetOwner> petownerData;
        public Repository<PetOwner> PetOwners
        {
            get
            {
                if (petownerData == null)
                    petownerData = new Repository<PetOwner>(context);
                return petownerData;
            }
        }

        private Repository<Classification> classificationData;
        public Repository<Classification> Classifications
        {
            get
            {
                if (classificationData == null)
                    classificationData = new Repository<Classification>(context);
                return classificationData;
            }
        }

        public void DeleteCurrentPetOwners(Pet pet)
        {
            var currentOwners = PetOwners.List(new QueryOptions<PetOwner>
            {
                Where = po => po.PetId == pet.PetId
            });
            foreach (PetOwner po in currentOwners)
            {
                PetOwners.Delete(po);
            }
        }

        public void LoadNewPetOwners(Pet pet, int[] ownerids)
        {
            foreach (int id in ownerids)
            {
                PetOwner po = new PetOwner { Pet = pet, OwnerId = id };
                PetOwners.Insert(po);
            }
        }

        public void Save() => context.SaveChanges();
    }
}
