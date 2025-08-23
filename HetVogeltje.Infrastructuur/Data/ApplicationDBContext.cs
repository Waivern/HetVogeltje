using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HetVogeltje.Domein.Entities;

namespace HetVogeltje.Infrastructuur.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber> VillaNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //   base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Villa>().HasData(
                
                new Villa { Id = 1, Name = "Vogeltje Villa", Description = "Villa het Vogeltje, is echt vogeltje", Price = 250.00, Sqft = 1500, Occupancy = 6, ImagePath = "https://placehold.co/600x400"},
                new Villa { Id = 2, Name = "Zon Villa", Description = "Zon Villa, is echt zon", Price = 300.00, Sqft = 1800, Occupancy = 8, ImagePath = "https://placehold.co/600x401" },
                new Villa { Id = 3, Name = "Ster Villa", Description = "Ster Villa, is echt ster", Price = 350.00, Sqft = 2000, Occupancy = 10, ImagePath = "https://placehold.co/600x402" },
                new Villa { Id = 4, Name = "Maan Villa", Description = "Maan Villa, is echt maan", Price = 400.00, Sqft = 2200, Occupancy = 12, ImagePath = "https://placehold.co/600x403" },
                new Villa { Id = 5, Name = "Budget Villa", Description = "Budget Villa, voor de minder bedeelden onder ons", Price = 150.00, Sqft = 100, Occupancy = 3, ImagePath = "https://placehold.co/600x404" }

                );

            modelBuilder.Entity<VillaNumber>().HasData(


                new VillaNumber { Villa_Number = 101, VillaId = 1, SpecialeDetails = "nummer 101" },
                new VillaNumber { Villa_Number = 102, VillaId = 2, SpecialeDetails = "nummer 102" },
                new VillaNumber { Villa_Number = 103, VillaId = 3, SpecialeDetails = "nummer 103" },
                new VillaNumber { Villa_Number = 104, VillaId = 4, SpecialeDetails = "speciale villa nummer 104" },
                new VillaNumber { Villa_Number = 105, VillaId = 5, SpecialeDetails = "105" }
                );
        }
    }
}
