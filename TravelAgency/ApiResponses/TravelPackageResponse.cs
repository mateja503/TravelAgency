using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace TravelAgency.ApiResponses
{
    public class TravelPackageResponse
    {
        public List<Place> places { get; set; }
    }

    public class Place
    {
        public int Position { get; set; }
        public string Title { get; set; } 
    
    }
}
