using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        public Customer? Customer { get; set; }
        public int CustomerId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than 0.")]
        public int Capacity { get; set; }
        public Itinerary? Itinerary { get; set; }
        public int ItineraryId { get; set; }
        public BookingStatus Status { get; set; }
        public DateRange DateRange { get; set; }
    }
}
