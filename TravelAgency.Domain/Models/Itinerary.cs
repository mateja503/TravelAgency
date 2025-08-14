using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Models
{
    public class Itinerary
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ItineraryActivity> ItineraryActivities { get; set; } = new List<ItineraryActivity>();
        public List<ItineraryTravelPackage> ItineraryTravelPackage { get; set; } = new List<ItineraryTravelPackage>(); 

        [NotMapped]
        public int SelectedTravelPackageId { get; set; }
        [NotMapped]
        public int SelectedActivityId { get; set; }
    }
}
