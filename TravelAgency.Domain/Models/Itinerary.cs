using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Models
{
    public class Itinerary
    {
        [Key]
        public int Id { get; set; }

        public List<ItineraryActivity> ItineraryActivities { get; set; }

        public List<ItineraryTravelPackage> ItineraryTravelPackage { get; set; }
    }
}
