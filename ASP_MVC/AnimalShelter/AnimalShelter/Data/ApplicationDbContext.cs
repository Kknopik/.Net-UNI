using AnimalShelter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AnimalShelter.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Adoption> Adoptions { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //override default names for login database
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("Identity");
            modelBuilder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable(name: "User");
            });
            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            // One -> Many : Animal -> MedicalRecords
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(m => m.Animal)
                .WithMany(a => a.MedicalRecords)
                .HasForeignKey(m => m.AnimalId)
                .OnDelete(DeleteBehavior.Cascade);

            // One -> One : Animal -> Adoption
            modelBuilder.Entity<Animal>()
                .HasOne(a => a.Adoption)
                .WithOne(ad => ad.Animal)
                .HasForeignKey<Adoption>(a => a.AnimalId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        // Fix adoption duplicate key upon changing status from "Adopted"
        public override int SaveChanges()
        {
            var changedAnimals = ChangeTracker.Entries<Animal>()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in changedAnimals)
            {
                var animal = entry.Entity;

                // Check if status changed from "Adopted"
                if (entry.OriginalValues["Status"].ToString() == StatusEnum.Adopted.ToString() &&
                    animal.Status != StatusEnum.Adopted)
                {
                    // Del adoption record
                    var adoption = Adoptions.FirstOrDefault(a => a.AnimalId == animal.Id);
                    if (adoption != null)
                    {
                        Adoptions.Remove(adoption);
                    }

                    animal.Adoption = null;
                }
            }

            return base.SaveChanges();
        }

    }
}
