using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Models
{
    public class ItineraryTravelPackage
    {
        [Key]
        public int Id { get; set; }
        public TravelPackage TravelPackage { get; set; }
        public int TravelPackageId { get; set; }
        public Itinerary Itinerary { get; set; }
        public int ItineraryId { get; set; }
    }
}
