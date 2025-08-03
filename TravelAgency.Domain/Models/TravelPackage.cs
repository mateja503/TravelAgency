using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.ValueObjects;

namespace TravelAgency.Domain.Models
{
    public class TravelPackage
    {
        [Key]
        public int Id { get; set; }
        public string Tittle { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public Price Price { get; set; }
        public DateRange DateRange { get; set; }
        public List<ItineraryTravelPackage>? ItineraryTravelPackage { get; set; }
    }
}
