using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Shared;

namespace TravelAgency.Domain.Models
{
    public class TravelActivity
    {
        [Key]
        public int Id { get; set; }
        public SeasonType SeasonType { get; set; }
        public string ActivityName { get; set; }
        public List<ItineraryActivity>? ItineraryActivities { get; set; }
    }
}
