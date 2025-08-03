using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;
using TravelAgency.Domain.Shared;
using TravelAgency.Domain.ValueObjects;

namespace TravelAgency.Domain.DTOs
{
    public record TravelPackageDto
    {
        public string Tittle { get; init; }
        public string Description { get; init; }
        public int Capacity { get; init; }
        public Price Price { get; init; }
        public DateRange DateRange { get; init; }
        public List<TravelActivityDto> TravelActivitiesList { get; init; }
    } 
    public record TravelActivityDto 
    {
        public SeasonType SeasonType { get; set; }
        public string ActivityName { get; set; }
    }
}
