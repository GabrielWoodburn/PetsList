using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Models
{
    internal class SeedPets : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> entity)
        {
            entity.HasData(
                new Pet { PetId = 1, Name = "Kumar", ClassificationId = "Cat", Weight = 12.10 },
                new Pet { PetId = 2, Name = "Max", ClassificationId = "Dog", Weight = 7.2 },
                new Pet { PetId = 3, Name = "Suzie", ClassificationId = "Bird", Weight = 0.8 },
                new Pet { PetId = 4, Name = "Mimi", ClassificationId = "Dog", Weight = 55.5 },
                new Pet { PetId = 5, Name = "Fluffy", ClassificationId = "Dog", Weight = 22.1 }
            );
        }
    }

}
