using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Models
{
    internal class SeedClassification : IEntityTypeConfiguration<Classification>
    {
        public void Configure(EntityTypeBuilder<Classification> entity)
        {
            entity.HasData(
                new Classification { ClassificationId = "dog", Name = "Dog" },
                new Classification { ClassificationId = "cat", Name = "Cat" },
                new Classification { ClassificationId = "bird", Name = "Bird" },
                new Classification { ClassificationId = "elephant", Name = "Elephant" },
                new Classification { ClassificationId = "kangaroo", Name = "Kangaroo" }              
            );
        }
    }
}
