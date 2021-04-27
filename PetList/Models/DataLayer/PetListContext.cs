using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Models
{
    public class PetListContext : DbContext
    {
        public PetListContext(DbContextOptions<PetListContext> options)
            : base(options)
        { }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetOwner> PetOwners { get; set; }
        public DbSet<Classification> Classifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // PetOwner: set primary key 
            modelBuilder.Entity<PetOwner>().HasKey(ba => new { ba.PetId, ba.OwnerId });

            // PetOwner: set foreign keys 
            modelBuilder.Entity<PetOwner>().HasOne(ba => ba.Pet)
                .WithMany(b => b.PetOwners)
                .HasForeignKey(ba => ba.PetId);
            modelBuilder.Entity<PetOwner>().HasOne(ba => ba.Owner)
                .WithMany(a => a.PetOwners)
                .HasForeignKey(ba => ba.OwnerId);

            // Pet remove cascading delete with Classification
            modelBuilder.Entity<Pet>().HasOne(b => b.Classification)
                .WithMany(g => g.Pets)
                .OnDelete(DeleteBehavior.Restrict);

            // initial data
            modelBuilder.ApplyConfiguration(new SeedClassification());
            modelBuilder.ApplyConfiguration(new SeedPets());
            modelBuilder.ApplyConfiguration(new SeedOwners());
            modelBuilder.ApplyConfiguration(new SeedPetOwners());
        }
    }
}
