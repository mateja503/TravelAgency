using TravelAgency.Domain.Shared;

namespace TravelAgency.ViewModels
{
    public record TravelPackageViewModel
    {
        public string Tittle { get; init; }

        public string Description { get; init; }

        public int Capacity { get; init; }

        public double Amount { get; init; }

        public TypeCurrency TypeCurrency { get; init; }

        public DateTime From { get; init; }

        public DateTime To { get; init; }

    }
}
