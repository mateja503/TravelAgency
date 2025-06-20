using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TravelAgency.Domain.Models;
using TravelAgency.Domain.ValueObjects;
using TravelAgency.Repository.Identity;

namespace TravelAgency.Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Itinerary> Itineraries { get; set; }

        public virtual DbSet<ItineraryActivity> ItineraryActivities { get; set; }

        public virtual DbSet<ItineraryTravelPackage> ItineraryTravelPackages { get; set; }

        public virtual DbSet<TravelActivity> TravelActivities { get; set; }

        public virtual DbSet<TravelPackage> TravelPackages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.OwnsOne(c => c.Address);
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.OwnsOne(b => b.DateRange);
            });

            modelBuilder.Entity<TravelPackage>(entity =>
            {
                entity.OwnsOne(t => t.Price);

                entity.OwnsOne(t => t.DateRange);
            });
        }


    }
}
