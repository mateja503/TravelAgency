using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.DTOs
{
    public class ItineraryDto
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public List<TravelActivityDto>? TravelActivities { get; set; }
        public List<TravelPackageDto>? TravelPackages { get; set; }

        public int? ItineraryTravelPackageId { get; set; }
        public int? ItineraryActivityId { get; set; }
        public TravelActivityDto TravelActivity { get; set; }

        public TravelPackageDto TravelPackage { get; set; }

        public int? SelectedTravelPackageId { get; set; }

        public int? SelectedActivityId { get; set; }
    }
}
