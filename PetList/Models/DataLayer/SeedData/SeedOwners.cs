using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PetList.Models
{
    internal class SeedOwners : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> entity)
        {
            entity.HasData(
                new Owner { OwnerId = 1, FirstName = "Sam", LastName = "Smith" },
                new Owner { OwnerId = 2, FirstName = "Calvin", LastName = "John" },
                new Owner { OwnerId = 3, FirstName = "Luke", LastName = "Jackson" },
                new Owner { OwnerId = 4, FirstName = "Jerry", LastName = "Springer" },
                new Owner { OwnerId = 5, FirstName = "Jack", LastName = "Smithson" }
            );
        }
    }
}
