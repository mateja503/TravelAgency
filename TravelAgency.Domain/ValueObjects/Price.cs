using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Shared;

namespace TravelAgency.Domain.ValueObjects
{
    public record Price
    {
        public double Amount { get; set; }

        public TypeCurrency TypeCurrencty { get; set; }
    }
}
