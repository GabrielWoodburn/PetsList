using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Models
{
    internal class SeedPetOwners : IEntityTypeConfiguration<PetOwner>
    {
        public void Configure(EntityTypeBuilder<PetOwner> entity)
        {
            entity.HasData(
                new PetOwner { PetId = 1, OwnerId = 11 },
                new PetOwner { PetId = 2, OwnerId = 33 },
                new PetOwner { PetId = 3, OwnerId = 18 },
                new PetOwner { PetId = 4, OwnerId = 55 },
                new PetOwner { PetId = 5, OwnerId = 99 }
            );
        }
    }

}
