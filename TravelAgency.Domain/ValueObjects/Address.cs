using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.ValueObjects
{
    public record Address
    {

        public string? Country { init; get; }

        public string? City { init; get; }

        public string? LivingAddress { init; get; }

    }
}
