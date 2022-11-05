using Domain.AggregatesModel.DroneAggregate;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DroneContext : DbContext, IUnitOfWork
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

            modelBuilder.Entity<Drone>().OwnsOne(e => e.DroneModel, add =>
            {
                add.Property(dm => dm.Name).HasColumnName(nameof(DroneModel.Name));
                add.Property(dm => dm.SerialNumber).HasColumnName(nameof(DroneModel.SerialNumber));
                add.WithOwner();
            });






        }
    }
}
