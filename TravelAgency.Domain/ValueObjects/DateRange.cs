using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.ValueObjects
{
    public record DateRange
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }
    }
}
