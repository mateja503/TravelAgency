using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Shared;
using TravelAgency.Domain.ValueObjects;

namespace TravelAgency.Domain.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public TravelPackage? TravelPackage { get; set; }
        public int? TravelPackageId { get; set; }
        public Itinerary? Itinerary { get; set; }
        public int? ItineraryId { get; set; }
        public BookingStatus Status { get; set; }
        public DateRange DateRange { get; set; }
    }
}
