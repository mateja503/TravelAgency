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
        public string Name { get; set; }
        public List<TravelActivityDto> TravelActivities { get; set; }
        public List<TravelPackageDto> TravelPackages { get; set; }
    }
}
