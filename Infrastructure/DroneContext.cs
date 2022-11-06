using Domain.AggregatesModel.DroneAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DroneContext : DbContext
    {

        public DroneContext()
        {
        }

        public DroneContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Drone> Drones { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drone>().ToTable("drones");
            modelBuilder.Entity<Drone>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            //Flatens DroneModel entity into the same table.
            //In real life it would be a good idea to store drone models as separate entities with their own IDs
            modelBuilder.Entity<Drone>().OwnsOne(e => e.DroneModel, add =>
            {
                add.Property(dm => dm.Name).HasColumnName(nameof(DroneModel.Name));
                add.Property(dm => dm.SerialNumber).HasColumnName(nameof(DroneModel.SerialNumber));
                add.WithOwner();
            });
        }
    }
}
