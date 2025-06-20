using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Models
{
    public class ItineraryActivity
    {
        [Key]
        public int Id { get; set; }

        public TravelActivity TravelActivity { get; set; }
        public int TravelActivityId { get; set; }

        public Itinerary Itinerary { get; set; }

        public int ItineraryId { get; set; }


    }
}
