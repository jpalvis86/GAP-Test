using Insurance.Core.Models;
using Insurance.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Insurance.Repository
{
    public class InsuranceDbContext : DbContext
    {
        public DbSet<RiskEntity> Risks { get; set; }
        public DbSet<InsuranceEntity> Insurances { get; set; }
        public DbSet<InsuranceCoverageBridgeEntity> InsurancesCoverages { get; set; }
        public DbSet<CoverageTypeEntity> CoverageTypes { get; set; }

        public InsuranceDbContext(DbContextOptions<InsuranceDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SetupInsurancesCoveragesBridgeEntity(modelBuilder);

            SeedRiskProfiles(modelBuilder);
            SeedCoverageTypes(modelBuilder);
            SeedInsurances(modelBuilder);
            SeedInsurancesCoverageTypes(modelBuilder);
        }

        private static void SetupInsurancesCoveragesBridgeEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InsuranceCoverageBridgeEntity>()
                .HasOne(ic => ic.Insurance)
                .WithMany(c => c.InsurancesCoverages)
                .HasForeignKey(ic => ic.InsuranceId);

            modelBuilder.Entity<InsuranceCoverageBridgeEntity>()
                .HasOne(ic => ic.CoverageType)
                .WithMany(c => c.InsurancesCoverages)
                .HasForeignKey(ic => ic.CoverageTypeId);

            modelBuilder.Entity<InsuranceCoverageBridgeEntity>()
                .HasKey(ic => new { ic.InsuranceId, ic.CoverageTypeId });
        }

        private static void SeedRiskProfiles(ModelBuilder modelBuilder)
        {
            var riskRecords = new List<RiskEntity>
            {
                new RiskEntity{ Id = 1, Name= "Low" },
                new RiskEntity{ Id = 2, Name= "Medium"},
                new RiskEntity{ Id = 3, Name= "Medium-High"},
                new RiskEntity{ Id = 4, Name= "High"},
            };

            modelBuilder.Entity<RiskEntity>().HasData(riskRecords);
        }

        private static void SeedCoverageTypes(ModelBuilder modelBuilder)
        {
            var coverageTypes = new List<CoverageTypeEntity>
            {
                new CoverageTypeEntity{ Id = 1, Name= "Earthquake" },
                new CoverageTypeEntity{ Id = 2, Name= "Fire"},
                new CoverageTypeEntity{ Id = 3, Name= "Robbery"},
                new CoverageTypeEntity{ Id = 4, Name= "Damage"},
                new CoverageTypeEntity{ Id = 5, Name= "Lost"}
            };

            modelBuilder.Entity<CoverageTypeEntity>().HasData(coverageTypes);
        } 
                
        private static void SeedInsurances(ModelBuilder modelBuilder)
        {
            var basicInsurances = new List<InsuranceEntity>
            {
                new InsuranceEntity
                {
                    Id = 1,
                    Name = "Low Fire 25",
                    Description = "Low Fire Insurance",
                    StartDate = new DateTime(2021,01,01),
                    MonthsOfCoverage = 12,
                    Price = 299.99M,
                    RiskId = (int)Risk.Low,
                    CoverageRate = 0.25

                },
                new InsuranceEntity
                {
                    Id = 2,
                    Name = "Medium Total 50",
                    Description = "Medium Total Insurance",
                    StartDate = new DateTime(2021,01,01),
                    MonthsOfCoverage = 36,
                    Price = 4999.99M,
                    RiskId = (int)Risk.Medium,
                    CoverageRate = 0.5
                },

            };

            modelBuilder.Entity<InsuranceEntity>().HasData(basicInsurances);
        }

        private static void SeedInsurancesCoverageTypes(ModelBuilder modelBuilder)
        {
            var coveragesByInsurance = new List<InsuranceCoverageBridgeEntity>
            {
                new InsuranceCoverageBridgeEntity{ InsuranceId = 1, CoverageTypeId = 1},
                new InsuranceCoverageBridgeEntity{ InsuranceId = 2, CoverageTypeId = 1},
                new InsuranceCoverageBridgeEntity{ InsuranceId = 2, CoverageTypeId = 2},
                new InsuranceCoverageBridgeEntity{ InsuranceId = 2, CoverageTypeId = 3},
                new InsuranceCoverageBridgeEntity{ InsuranceId = 2, CoverageTypeId = 4},
                new InsuranceCoverageBridgeEntity{ InsuranceId = 2, CoverageTypeId = 5}
                
            };

            modelBuilder.Entity<InsuranceCoverageBridgeEntity>().HasData(coveragesByInsurance);
        }
    }
}
